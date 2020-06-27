using System;
using System.Linq;
using TerminalWeb.Domain.Entities;

namespace TerminalWeb.Domain.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T entity);
        IQueryable<T> GetAll();
        T GetById(Guid id);
        void Update(T entity);
    }
}
