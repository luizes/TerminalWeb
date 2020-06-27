using Microsoft.EntityFrameworkCore;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Infra.EntityTypeConfigurations;

namespace TerminalWeb.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<DiskDrive> DiskDrives { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MachineEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new DiskDriveEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntityTypeConfiguration());
        }
    }
}
