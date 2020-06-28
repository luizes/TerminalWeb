using System;
using System.Collections.Generic;

namespace TerminalWeb.Domain.Entities
{
    public sealed class Machine : Entity
    {
        public Machine(Guid id, string name, string ipLocal, bool antivirusInstalled, bool firewallIsActive, string windowsVersion)
            : base(id)
        {
            Name = name;
            IpLocal = ipLocal;
            AntivirusInstalled = antivirusInstalled;
            FirewallIsActive = firewallIsActive;
            WindowsVersion = windowsVersion;
            DiskDrives = new List<DiskDrive>();
        }

        public string Name { get; private set; }
        public string IpLocal { get; private set; }
        public bool AntivirusInstalled { get; private set; }
        public bool FirewallIsActive { get; private set; }
        public string WindowsVersion { get; private set; }
        public List<DiskDrive> DiskDrives { get; private set; }

        public void AddDiskDrive(string name, long totalSize)
        {
            DiskDrives.Add(new DiskDrive(Id, name, totalSize));
        }
    }
}
