using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TerminalWeb.Domain.Commands;

namespace TerminalWeb.Test.CommandTests
{
    [TestClass]
    public class CreateMachineCommandTests
    {
        private readonly CreateMachineCommand _invalidCommand = new CreateMachineCommand("", "", false, false, "", null);
        private readonly CreateMachineCommand _validCommand = new CreateMachineCommand("Notebook", "192.168.0.1", false, true, "Windows 10 PRO - 1903", new List<(string name, long totalSize)>
            {
                ("C:\\", 165124),
                ("D:\\", 127498)
            });

        public CreateMachineCommandTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [TestMethod]
        public void DadoUmComandoInvalido()
        {
            Assert.AreEqual(_invalidCommand.Valid, false);
        }

        [TestMethod]
        public void DadoUmComandoValido()
        {
            Assert.AreEqual(_validCommand.Valid, true);
        }
    }
}
