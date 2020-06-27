using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.Commands.Results;
using TerminalWeb.Domain.Handles;
using TerminalWeb.Test.Repositories;

namespace TerminalWeb.Test.HandlerTests
{
    [TestClass]
    public class MachineHandlerTests
    {
        private readonly MachineHandler _handler = new MachineHandler(new FakeMachineRepository());
        private readonly CreateMachineCommand _invalidCommand = new CreateMachineCommand("", "", false, false, "", null);
        private readonly CreateMachineCommand _validCommand = new CreateMachineCommand("Notebook", "192.168.0.1", false, true, "Windows 10 PRO - 1903", new List<(string name, long totalSize)>
            {
                ("C:\\", 165124),
                ("D:\\", 127498)
            });
        private GenericCommandResult _result = new GenericCommandResult();

        [TestMethod]
        public void DadoUmComandoInvalidoDeveInterromperExecucao()
        {
            _result = (GenericCommandResult)_handler.Handle(_invalidCommand);

            Assert.AreEqual(_result.Success, false);
        }

        [TestMethod]
        public void DadoUmComandoInvalidoDeveCriarUmaMaquina()
        {
            _result = (GenericCommandResult)_handler.Handle(_validCommand);

            Assert.AreEqual(_result.Success, true);
        }
    }
}
