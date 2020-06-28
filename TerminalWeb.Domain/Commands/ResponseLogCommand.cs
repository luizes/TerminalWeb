using Flunt.Notifications;
using Flunt.Validations;
using System;
using TerminalWeb.Domain.Commands.Contracts;

namespace TerminalWeb.Domain.Commands
{
    public sealed class ResponseLogCommand : Notifiable, ICommand
    {
        public ResponseLogCommand() { }

        public ResponseLogCommand(Guid logId, string response, bool finish = false)
        {
            LogId = logId;
            Response = response;
            Finish = finish;
        }

        public Guid LogId { get; set; }
        public string Response { get; set; }
        public bool Finish { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract().Requires());
        }
    }
}
