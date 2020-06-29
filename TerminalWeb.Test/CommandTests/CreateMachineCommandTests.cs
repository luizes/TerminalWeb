using NUnit.Framework;
using System;
using TerminalWeb.Domain.Commands;
using TerminalWeb.Domain.ViewModels;

namespace TerminalWeb.Test.CommandTests
{
    public class CreateMachineCommandTests
    {
        private readonly CreateMachineCommand _invalidCommand = new CreateMachineCommand(Guid.NewGuid(), "", "", false, false, "", null);
        private readonly CreateMachineCommand _validCommand = new CreateMachineCommand(Guid.NewGuid(), "Notebook", "192.168.0.1", false, true, "Windows 10 PRO - 1903", new DiskDriveViewModel[]
            {
                new DiskDriveViewModel("C:\\", 165124),
                new DiskDriveViewModel("D:\\", 127498)
            });

        public CreateMachineCommandTests()
        {
            _invalidCommand.Validate();
            _validCommand.Validate();
        }

        [Test]
        public void DadoUmComandoInvalido()
        {
            Assert.AreEqual(_invalidCommand.Valid, false);
        }

        [Test]
        public void DadoUmComandoValido()
        {
            Assert.AreEqual(_validCommand.Valid, true);
        }
    }
}
