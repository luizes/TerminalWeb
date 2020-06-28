using System;

namespace TerminalWeb.Domain.Entities
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.Now;
        }

        protected Entity(Guid id)
        {
            Id = id;
            CreatedAt = DateTimeOffset.Now;
        }

        public Guid Id { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        public bool Equals(Entity other) => Id == other.Id;
    }
}
