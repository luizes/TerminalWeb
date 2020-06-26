using System.Linq;
using TerminalWeb.Models;

namespace TerminalWeb.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        IQueryable<Log> GetAllByMachineId(int machineId);
    }
}
