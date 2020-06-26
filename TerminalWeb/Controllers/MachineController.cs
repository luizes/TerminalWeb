using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TerminalWeb.Models;
using TerminalWeb.Repositories;

namespace TerminalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Machine>> Get([FromServices] IMachineRepository repository)
        {
            var machines = repository.GetAll().ToList();

            return machines;
        }

        [HttpPost]
        public ActionResult<Machine> Post([FromServices] IMachineRepository repository, [FromBody] Machine machine)
        {
            if (ModelState.IsValid)
            {
                repository.Create(machine);

                return machine;
            }

            return BadRequest(ModelState);
        }
    }
}
