using System;
using System.ComponentModel.DataAnnotations;

namespace TerminalWeb.Models.Base
{
    public abstract class Entity
    {
        public Entity() => CreatedAt = DateTimeOffset.Now;

        [Key]
        public Guid Id { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
    }
}
