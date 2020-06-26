using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TerminalWeb.Client
{
    class Worker : BackgroundService
    {
        private HubConnection machineConnection;
        private HubConnection logConnection;

        public Worker()
        {
            machineConnection = CreateHubConnectionStarted("machineHub");
            logConnection = CreateHubConnectionStarted("logHub");
        }

        private static HubConnection CreateHubConnectionStarted(string endpoint)
        {
            var hubConnectionBuilder = new HubConnectionBuilder().WithUrl($"http://localhost:44395/{endpoint}").Build();

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
            {
                Console.WriteLine("Worker running...");
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
