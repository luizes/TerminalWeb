using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TerminalWeb.Models;
using TerminalWeb.Repositories;

namespace TerminalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _repository;

        public LogController(ILogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{machineId:int}")]
        public ActionResult<List<Log>> Get(int machineId) => _repository.GetAllByMachineId(machineId).ToList();

        [HttpPost]
        public ActionResult<Log> Post([FromBody] Log log)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(log);

                return log;
            }

            return BadRequest(ModelState);
        }
    }
}
