using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TerminalWeb.Models;
using TerminalWeb.Repositories;

namespace TerminalWeb.Hubs
{
    public sealed class MachineHub : Hub
    {
        private readonly IMachineRepository _repository;

        public MachineHub(IMachineRepository repository) => _repository = repository;

        public async Task Create(Machine machine)
        {
            _repository.Create(machine);

            await Clients.All.SendAsync("NewMachine", machine);
        }
    }
}
