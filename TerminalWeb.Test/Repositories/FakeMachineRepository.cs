using System;
using System.Linq;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;

namespace TerminalWeb.Test.Repositories
{
    public class FakeMachineRepository : IMachineRepository
    {
        public void Create(Machine entity)
        {

        }

        public IQueryable<Machine> GetAll()
        {
            return null;
        }

        public Machine GetById(Guid id)
        {
            return new Machine("", "", false, false, "");
        }

        public void Update(Machine entity)
        {

        }
    }
}
