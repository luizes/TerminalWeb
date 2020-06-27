using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;
using TerminalWeb.Infra.Context;

namespace TerminalWeb.Infra.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        public virtual void Create(T entity)
        {
            _context.Add(entity);
            SaveChanges();
        }

        public virtual IQueryable<T> GetAll() => _context.Set<T>().AsNoTracking().OrderBy(e => e.CreatedAt);

        public T GetById(Guid id) => _context.Set<T>().FirstOrDefault(e => e.Id == id);

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        private void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
