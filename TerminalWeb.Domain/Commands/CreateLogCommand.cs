using Flunt.Notifications;
using Flunt.Validations;
using System;
using TerminalWeb.Domain.Commands.Contracts;

namespace TerminalWeb.Domain.Commands
{
    public sealed class CreateLogCommand : Notifiable, ICommand
    {
        public CreateLogCommand() { }

        public CreateLogCommand(Guid machineId, string command)
        {
            MachineId = machineId;
            Command = command;
        }

        public Guid MachineId { get; set; }
        public string Command { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract().Requires());
        }
    }
}
