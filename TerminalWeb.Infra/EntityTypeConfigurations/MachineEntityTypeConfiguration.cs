using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TerminalWeb.Domain.Entities;

namespace TerminalWeb.Infra.EntityTypeConfigurations
{
    public class MachineEntityTypeConfiguration : IEntityTypeConfiguration<Machine>
    {
        public void Configure(EntityTypeBuilder<Machine> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.IpLocal).IsRequired().HasMaxLength(40);
            builder.Property(x => x.AntivirusInstalled);
            builder.Property(x => x.FirewallIsActive);
            builder.Property(x => x.WindowsVersion).IsRequired().HasMaxLength(60);
            builder.HasMany(x => x.DiskDrives);
        }
    }
}
