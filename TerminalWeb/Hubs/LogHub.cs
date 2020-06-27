using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Results;
using TerminalWeb.Domain.Handles;

namespace TerminalWeb.Hubs
{
    public class LogHub : Hub
    {
        private readonly LogCommandHandler _handler;

        public LogHub(LogCommandHandler handler)
        {
            _handler = handler;
        }

        public async Task Send(CreateLogCommand command)
        {
            var result = (GenericCommandResult)_handler.Handle(command);

            await Clients.All.SendAsync("NewLog", result);
        }

        public async Task Response(ResponseLogCommand command)
        {
            var result = (GenericCommandResult)_handler.Handle(command);

            await Clients.All.SendAsync("ResponseLog", result);
        }
    }
}
