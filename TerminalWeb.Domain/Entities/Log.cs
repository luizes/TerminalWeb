using System;

namespace TerminalWeb.Domain.Entities
{
    public sealed class Log : Entity
    {
        public Log(Guid machineId, string command)
        {
            MachineId = machineId;
            Command = command;
        }

        public Guid MachineId { get; private set; }
        public string Command { get; private set; }
        public string Response { get; private set; }
        public bool Finish { get; private set; }

        public void SetResponse(string response)
        {
            Response = response;
        }

        public void MarkAsFinish()
        {
            Finish = true;
        }
    }
}
