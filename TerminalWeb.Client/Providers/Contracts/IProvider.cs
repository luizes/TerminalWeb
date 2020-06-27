using TerminalWeb.Domain.Commands.Contracts;

namespace TerminalWeb.Client.Providers.Contracts
{
    public interface IProvider<T> where T : ICommand
    {
        T Generate();
    }
}
