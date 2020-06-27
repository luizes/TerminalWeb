using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;

namespace TerminalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly IMachineRepository _repository;

        public MachineController(IMachineRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<List<Machine>> Get() => _repository.GetAll().ToList();
    }
}
