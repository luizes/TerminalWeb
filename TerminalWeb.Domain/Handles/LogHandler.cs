using Flunt.Notifications;
using System;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Contracts;
using TerminalWeb.Domain.Commands.Results;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Handles.Contracts;
using TerminalWeb.Domain.Repositories;

namespace TerminalWeb.Domain.Handles
{
    public sealed class LogHandler : Notifiable, IHandler<CreateLogCommand>, IHandler<ResponseLogCommand>, IHandler<FinishLogCommand>
    {
        private readonly ILogRepository _repository;

        public LogHandler(ILogRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateLogCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Não será possível enviar esse comando!", command.Notifications);

            var log = new Log(command.MachineId, command.Command);

            _repository.Create(log);

            return new GenericCommandResult(true, "Comando enviado!", log);
        }

        public ICommandResult Handle(ResponseLogCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Não será possível responder esse comando!", command.Notifications);

            var log = GetLog(command.LogId);

            log.SetResponse(command.Response);

            _repository.Update(log);

            return new GenericCommandResult(true, "Comando respondido!", log);
        }

        public ICommandResult Handle(FinishLogCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Não será possível finalizar esse comando!", command.Notifications);

            var log = GetLog(command.LogId);

            log.MarkAsFinish();

            _repository.Update(log);

            return new GenericCommandResult(true, "Comando finalizado!", log);
        }

        private Log GetLog(Guid logId) => _repository.GetById(logId);
    }
}
