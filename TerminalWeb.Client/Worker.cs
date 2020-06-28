using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Management.Automation;
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
        private readonly HubConnection _machineConnection;
        private readonly HubConnection _logConnection;
        private readonly CreateMachineCommand _machine = new MachineProvider().Generate();
        private readonly PowerShell powerShell = PowerShell.Create();

        public Worker()
        {
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
                _logConnection.On("NewLog", ProccessLog());
            });
        }

        private Action<GenericCommandResult> ProccessLog() => message =>
        {
            if (!message.Success)
                return;

            var log = (JsonElement)message.Data;
            var machineId = new Guid(log.GetProperty("machineId").ToString());

            if (_machine.Id == machineId)
                return;

            var logId = new Guid(log.GetProperty("id").ToString());
            var command = log.GetProperty("command").ToString();
            var sb = new StringBuilder();
            var outputCollection = new PSDataCollection<PSObject>();

            powerShell.AddScript(command);

            outputCollection.DataAdded += (sender, args) =>
            {
                var teste = ((PSDataCollection<PSObject>)sender)[args.Index];
                var teste2 = teste.ToString();
                sb.AppendLine(teste2);
                _logConnection.SendAsync("Response", new ResponseLogCommand(logId, sb.ToString()));
            };

            powerShell.Invoke(null, outputCollection, null);

            _logConnection.SendAsync("Finish", new FinishLogCommand(logId));
        };

        private static HubConnection CreateHubConnection(string endpoint)
        {
            var hubConnectionBuilder = new HubConnectionBuilder()
                .WithUrl(new Uri($"http://localhost:51554/{endpoint}"))
                .WithAutomaticReconnect()
                .Build();

            hubConnectionBuilder.Closed += async (error) =>
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
