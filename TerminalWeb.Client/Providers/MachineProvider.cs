using System;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using TerminalWeb.Client.Providers.Contracts;
using TerminalWeb.Domain.Commands;

namespace TerminalWeb.Client.Providers
{
    public class MachineProvider : IProvider<CreateMachineCommand>
    {
        public CreateMachineCommand Generate()
        {
            var name = Environment.MachineName;
            var ipLocal = Dns.GetHostAddresses(Dns.GetHostName())[4].ToString();
            var antivirusInstalled = AntivirusInstalled();
            dynamic firewallManager = Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwMgr"));
            var firewallIsActive = firewallManager.LocalPolicy.CurrentProfile.FirewallEnabled;
            var windowsVersion = Environment.OSVersion.VersionString;
            var diskDrives = DriveInfo.GetDrives().Select(drive => (drive.Name, drive.TotalSize));

            return new CreateMachineCommand(name, ipLocal, antivirusInstalled, firewallIsActive, windowsVersion, diskDrives);
        }

        private bool AntivirusInstalled()
        {
            string wmipathstr = @"\\" + Environment.MachineName + @"\root\SecurityCenter";

            try
            {
                var searcher = new ManagementObjectSearcher(wmipathstr, "SELECT * FROM AntivirusProduct");
                var instances = searcher.Get();

                return instances.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine((ex.InnerException ?? ex).Message);
            }

            return false;
        }
    }
}
