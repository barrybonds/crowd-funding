using System.Threading.Tasks;
using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;

namespace CrowdFunding.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    [Route("api/endeavors")]
    [Route("api/{v:apiversion}/endeavors")]

    [ApiController]
    public class EndeavourV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public EndeavourV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies([FromQuery] EndeavourParameters endeavourParameters)
        {
            var endavours = await _repository.Endeavour.GetAllEndeavoursAsync(endeavourParameters,trackChanges: false);
            return Ok(endavours);
        }
    }
}