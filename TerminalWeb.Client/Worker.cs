using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
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

            var logId = new Guid(log.GetProperty("id").ToString());
            //var command = log.GetProperty("command").ToString();

            _logConnection.SendAsync("Response", new ResponseLogCommand(logId, "Ok"));
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
