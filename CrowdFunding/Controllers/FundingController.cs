using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [Route("api/funding")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FundingController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;


        public FundingController(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult InitializeTransaction([FromBody] InitializeTransaction transaction)
        {
            throw new NotImplementedException();
        }


    }
}