using Flunt.Notifications;
using Flunt.Validations;
using System;
using TerminalWeb.Domain.Commands.Contracts;

namespace TerminalWeb.Domain.Commands
{
    public sealed class FinishLogCommand : Notifiable, ICommand
    {
        public FinishLogCommand() { }

        public FinishLogCommand(Guid logId)
        {
            LogId = logId;
        }

        public Guid LogId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract().Requires());
        }
    }
}
