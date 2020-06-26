using Microsoft.EntityFrameworkCore;
using System.Linq;
using TerminalWeb.Data;
using TerminalWeb.Models;

namespace TerminalWeb.Repositories.Implementations
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        public LogRepository(StoreDataContext context) : base(context)
        {
        }

        public override IQueryable<Log> GetAll() => base.GetAll().AsNoTracking();

        public IQueryable<Log> GetAllByMachineId(int machineId) => GetAll().Where(l => l.MachineId == machineId);
    }
}
