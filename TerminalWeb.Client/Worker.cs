using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TerminalWeb.Client.Providers;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Results;
using TerminalWeb.Domain.Entities;

namespace TerminalWeb.Client
{
    class Worker : BackgroundService
    {
        private readonly MachineProvider _machineProvider = new MachineProvider();
        private Machine _machine;
        private readonly HubConnection _machineConnection;
        private readonly HubConnection _logConnection;

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
                await _machineConnection.SendAsync("Create", _machineProvider.Generate());

                await _logConnection.StartAsync();
                _logConnection.On("NewLog", (Func<GenericCommandResult, Task>)(async message =>
                {
                    if (!message.Success)
                        return;

                    var log = (JsonElement)message.Data;
                    var logId = new Guid(log.GetProperty("id").ToString());
                    var command = log.GetProperty("command").ToString();

                    await _logConnection.SendAsync("Response", new ResponseLogCommand(logId, "respondi"));
                }));
            });
        }

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
