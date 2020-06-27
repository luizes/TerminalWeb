using Microsoft.EntityFrameworkCore;
using System.Linq;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;
using TerminalWeb.Infra.Context;

namespace TerminalWeb.Infra.Repositories
{
    public class MachineRepository : Repository<Machine>, IMachineRepository
    {
        public MachineRepository(DataContext context) 
            : base(context)
        {

        }

        public override IQueryable<Machine> GetAll() => base.GetAll().Include(m => m.DiskDrives);
    }
}
