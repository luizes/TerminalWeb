using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;

namespace TerminalWeb.Hubs
{
    public class LogHub : Hub
    {
        private readonly ILogRepository _repository;

        public LogHub(ILogRepository repository)
        {
            _repository = repository;
        }

        public async Task Send(Log log)
        {
            _repository.Create(log);

            await Clients.All.SendAsync("NewLog", log);
        }

        public async Task Response(Log log)
        {
            _repository.Update(log);

            await Clients.All.SendAsync("ResponseLog", log);
        }
    }
}
