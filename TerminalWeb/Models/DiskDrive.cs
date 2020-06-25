using System.ComponentModel.DataAnnotations;
using TerminalWeb.Models.Base;

namespace TerminalWeb.Models
{
    public sealed class DiskDrive : Entity
    {
        [Required]
        [MaxLength(5)]
        public string Name { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long TotalSize { get; set; }

        [Required]
        public int MachineId { get; set; }
    }
}
