using System.Linq;
using TerminalWeb.Data;
using TerminalWeb.Models.Base;

namespace TerminalWeb.Repositories.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly StoreDataContext _context;

        public Repository(StoreDataContext context) => _context = context;

        public virtual void Create(Entity entity)
        {
            _context.Add(entity);
            SaveChanges();
        }

        public virtual IQueryable<T> GetAll() => _context.Set<T>().OrderBy(e => e.CreatedAt);

        public void Update(Entity entity)
        {
            _context.Update(entity);
            SaveChanges();
        }

        private void SaveChanges() => _context.SaveChanges();
    }
}
