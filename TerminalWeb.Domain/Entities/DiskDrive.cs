using System;

namespace TerminalWeb.Domain.Entities
{
    public sealed class DiskDrive : Entity
    {
        public DiskDrive(Guid id, string name, long totalSize)
        {
            MachineId = id;
            Name = name;
            TotalSize = totalSize;
        }

        public string Name { get; private set; }
        public long TotalSize { get; private set; }
        public Guid MachineId { get; private set; }
    }
}
