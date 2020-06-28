using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Handles;

namespace TerminalWeb.Hubs
{
    public class LogHub : Hub
    {
        private readonly LogHandler _handler;

        public LogHub(LogHandler handler)
        {
            _handler = handler;
        }

        public async Task Send(CreateLogCommand command)
        {
            var result = _handler.Handle(command);

            await Clients.All.SendAsync("NewLog", result);
        }

        public async Task Response(ResponseLogCommand command)
        {
            var result = _handler.Handle(command);

            await Clients.All.SendAsync("ResponseLog", result);
        }

        public async Task Finish(FinishLogCommand command)
        {
            var result = _handler.Handle(command);

            await Clients.All.SendAsync("ResponseLog", result);
        }
    }
}
