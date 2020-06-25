using Microsoft.AspNetCore.Mvc;
using TerminalWeb.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TerminalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        // GET api/<LogController>/5
        [HttpGet("{id:int}")]
        public string Get(int machineId)
        {
            return "value";
        }

        // POST api/<LogController>
        [HttpPost]
        public void Post([FromBody] Log log)
        {
        }
    }
}
