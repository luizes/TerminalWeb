using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Results;
using TerminalWeb.Domain.Handles;

namespace TerminalWeb.Hubs
{
    public sealed class MachineHub : Hub
    {
        private readonly MachineHandler _handler;

        public MachineHub(MachineHandler handler)
        {
            _handler = handler;
        }

        public async Task Create(CreateMachineCommand command)
        {
            var result = (GenericCommandResult)_handler.Handle(command);

            await Clients.All.SendAsync("NewMachine", result);
        }
    }
}
