using System.Linq;
using TerminalWeb.Models.Base;

namespace TerminalWeb.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Create(Entity entity);
        IQueryable<T> GetAll();
        void Update(Entity entity);
    }
}
