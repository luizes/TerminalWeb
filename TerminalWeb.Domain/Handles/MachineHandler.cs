using Flunt.Notifications;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Contracts;
using TerminalWeb.Domain.Commands.Results;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Handles.Contracts;
using TerminalWeb.Domain.Repositories;

namespace TerminalWeb.Domain.Handles
{
    public sealed class MachineHandler : Notifiable, IHandler<CreateMachineCommand>
    {
        private readonly IMachineRepository _repository;

        public MachineHandler(IMachineRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(CreateMachineCommand command)
        {
            command.Validate();

            if (command.Invalid)
                return new GenericCommandResult(false, "Não será possível integrar a máquina no sistema!", command.Notifications);

            var machine = new Machine(command.Name, command.IpLocal, command.AntivirusInstalled, command.FirewallIsActive, command.WindowsVersion);

            foreach ((string Name, long TotalSize) in command.DiskDrives)
                machine.AddDiskDrive(Name, TotalSize);

            _repository.Create(machine);

            return new GenericCommandResult(true, "Máquina integrada!", machine);
        }
    }
}
