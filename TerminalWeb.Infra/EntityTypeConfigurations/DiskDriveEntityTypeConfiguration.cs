using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TerminalWeb.Domain.Entities;

namespace TerminalWeb.Infra.EntityTypeConfigurations
{
    public class DiskDriveEntityTypeConfiguration : IEntityTypeConfiguration<DiskDrive>
    {
        public void Configure(EntityTypeBuilder<DiskDrive> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.MachineId).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.TotalSize).IsRequired();
        }
    }
}
