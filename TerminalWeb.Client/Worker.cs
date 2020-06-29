using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TerminalWeb.Client.Providers;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Results;

namespace TerminalWeb.Client
{
    class Worker : BackgroundService
    {
        private readonly CreateMachineCommand _machine;
        private readonly Process _process;
        private readonly HubConnection _machineConnection;
        private readonly HubConnection _logConnection;
        private bool fristCommand = true;

        public Worker()
        {
            _machine = new MachineProvider().Generate();

            _process = Process.Start(new ProcessStartInfo("powershell.exe")
            {
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                WorkingDirectory = _machine.DiskDrives[0].Name
            });

            _machineConnection = CreateHubConnection("machineHub");
            _logConnection = CreateHubConnection("logHub");

            CreateMachineAndStartListeningAsync();
        }

        private void CreateMachineAndStartListeningAsync()
        {
            Task.Run(async () =>
            {
                await _machineConnection.StartAsync();
                await _machineConnection.SendAsync("Create", _machine);
                await _logConnection.StartAsync();
                _logConnection.On("NewLog", ProcessLog());
            });
        }

        private Action<GenericCommandResult> ProcessLog() => message =>
        {
            if (!message.Success)
                return;

            var log = (JsonElement)message.Data;
            var machineId = new Guid(log.GetProperty("machineId").ToString());

            if (_machine.Id != machineId)
                return;

            var command = log.GetProperty("command").ToString();
            var logId = new Guid(log.GetProperty("id").ToString());
            var stringBuilder = new StringBuilder();

            _process.OutputDataReceived += ResponseLog(logId, stringBuilder);
            _process.StandardInput.WriteLine(command);

            if (fristCommand)
            {
                _process.BeginOutputReadLine();
                fristCommand = false;
            }

            _logConnection.SendAsync("Finish", new FinishLogCommand(logId));
        };

        private DataReceivedEventHandler ResponseLog(Guid logId, StringBuilder stringBuilder) => (sender, e) =>
        {
            stringBuilder.AppendLine(e.Data);
            _logConnection.SendAsync("Response", new ResponseLogCommand(logId, stringBuilder.ToString()));
        };

        private static HubConnection CreateHubConnection(string endpoint)
        {
            var hubConnectionBuilder = new HubConnectionBuilder()
                .WithUrl(new Uri($"http://localhost:51554/{endpoint}"))
                .WithAutomaticReconnect()
                .Build();

            hubConnectionBuilder.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await hubConnectionBuilder.StartAsync();
            };

            return hubConnectionBuilder;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
                await Task.Delay(1000, cancellationToken);

            Console.WriteLine("Worker stopping...");
        }
    }
}
