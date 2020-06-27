using TerminalWeb.Domain.Commands.Contracts;

namespace TerminalWeb.Domain.Handles.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
