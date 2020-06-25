using System.ComponentModel.DataAnnotations;
using TerminalWeb.Models.Base;

namespace TerminalWeb.Models
{
    public sealed class Log : Entity
    {
        [Required]
        public string Command { get; set; }

        [Required]
        public string Response { get; set; }

        [Required]
        public int MachineId { get; set; }
    }
}
