using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TerminalWeb.Domain.Entities;
using TerminalWeb.Domain.Repositories;

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

        [HttpGet("{machineId}")]
        public ActionResult<List<Log>> Get(Guid machineId) => _repository.GetAllByMachineId(machineId).ToList();
    }
}
