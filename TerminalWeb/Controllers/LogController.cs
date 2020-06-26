﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{machineId:int}")]
        public ActionResult<List<Log>> Get([FromServices] ILogRepository repository, int machineId) => repository.GetAllByMachineId(machineId).ToList();

        [HttpPost]
        public ActionResult<Log> Post([FromServices] ILogRepository repository, [FromBody] Log log)
        {
            if (ModelState.IsValid)
            {
                repository.Create(log);

                return log;
            }

            return BadRequest(ModelState);
        }
    }
}