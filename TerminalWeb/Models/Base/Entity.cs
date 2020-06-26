using System;
using System.ComponentModel.DataAnnotations;

namespace TerminalWeb.Models.Base
{
    public abstract class Entity
    {
        public Entity() => CreatedAt = DateTimeOffset.Now;

        [Key]
        public int Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
