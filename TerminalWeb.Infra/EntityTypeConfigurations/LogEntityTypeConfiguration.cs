using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TerminalWeb.Domain.Entities;

namespace TerminalWeb.Infra.EntityTypeConfigurations
{
    class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.Property(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.MachineId).IsRequired();
            builder.Property(x => x.Command).IsRequired();
            builder.Property(x => x.Response);
            builder.Property(x => x.Finish);
        }
    }
}
