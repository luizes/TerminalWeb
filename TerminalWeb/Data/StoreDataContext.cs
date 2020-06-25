using Microsoft.EntityFrameworkCore;
using TerminalWeb.Models;

namespace TerminalWeb.Data
{
    public class StoreDataContext : DbContext
    {
        public StoreDataContext(DbContextOptions<StoreDataContext> options)
            : base(options)
        {

        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<DiskDrive> DiskDrives { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
