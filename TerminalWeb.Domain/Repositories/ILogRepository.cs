using System;
using System.Linq;
using TerminalWeb.Domain.Entities;

namespace TerminalWeb.Domain.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        IQueryable<Log> GetAllByMachineId(Guid machineId);
    }
}
