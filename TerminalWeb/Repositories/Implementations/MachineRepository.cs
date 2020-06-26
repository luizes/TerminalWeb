using Microsoft.EntityFrameworkCore;
using System.Linq;
using TerminalWeb.Data;
using TerminalWeb.Models;

namespace TerminalWeb.Repositories.Implementations
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        public MachineRepository(StoreDataContext context) : base(context)
        {
        }

        public override IQueryable<Machine> GetAll() => base.GetAll().Include(m => m.DiskDrives).AsNoTracking();
    }
}
