using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TerminalWeb.Models.Base;

namespace TerminalWeb.Models
{
    public sealed class Machine : Entity
    {
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        public string IPLocal { get; set; }

        public bool AntivirusInstalled { get; set; }

        public bool FirewallIsActive { get; set; }

        [Required]
        public string WindowsVersion { get; set; }

        [Required]
        public IEnumerable<DiskDrive> DiskDrives { get; set; }

        public IEnumerable<Log> Logs { get; set; }
    }
}
