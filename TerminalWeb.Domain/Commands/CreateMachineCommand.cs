using Flunt.Notifications;
using Flunt.Validations;
using System;
using TerminalWeb.Domain.Commands.Contracts;
using TerminalWeb.Domain.ViewModels;

namespace TerminalWeb.Domain.Commands
{
    public sealed class CreateMachineCommand : Notifiable, ICommand
    {
        public CreateMachineCommand() { }

        public CreateMachineCommand(Guid id, string name, string ipLocal, bool antivirusInstalled, bool firewallIsActive, string windowsVersion, DiskDriveViewModel[] diskDrives)
        {
            Id = id;
            Name = name;
            IpLocal = ipLocal;
            AntivirusInstalled = antivirusInstalled;
            FirewallIsActive = firewallIsActive;
            WindowsVersion = windowsVersion;
            DiskDrives = diskDrives;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IpLocal { get; set; }
        public bool AntivirusInstalled { get; set; }
        public bool FirewallIsActive { get; set; }
        public string WindowsVersion { get; set; }
        public DiskDriveViewModel[] DiskDrives { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Name, 1, "Name", "Nome da máquina é invalido.")
            );
        }
    }
}
