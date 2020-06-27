using System;
using System.Linq;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;
using TerminalWeb.Infra.Context;

namespace TerminalWeb.Infra.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(DataContext context)
            : base(context)
        {

        }

        public IQueryable<Log> GetAllByMachineId(Guid machineId) => GetAll().Where(l => l.MachineId == machineId);
    }
}
