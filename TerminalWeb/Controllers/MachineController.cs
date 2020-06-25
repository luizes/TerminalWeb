using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TerminalWeb.Data;
using TerminalWeb.Models;

namespace TerminalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        // GET: api/<MachineController> 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Machine>>> GetAsync([FromServices] StoreDataContext context)
        {
            var machines = await context.Machines.ToListAsync();

            return machines;
        }

        // POST api/<MachineController>
        [HttpPost]
        public async Task<ActionResult<Machine>> Post([FromServices] StoreDataContext context, [FromBody] Machine machine)
        {
            if (ModelState.IsValid)
            {
                context.Machines.Add(machine);
                await context.SaveChangesAsync();

                return machine;
            }

            return BadRequest(ModelState);
        }
    }
}
