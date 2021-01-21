using AutoMapper;
using CrowdFunding.ActionFilters;
//using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
//using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Newtonsoft.Json;
using CrowdFunding.ActionFiltersz;
using Marvin.Cache.Headers;

namespace CrowdFunding.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/endeavors")]
    [ApiController]
    [ResponseCache(CacheProfileName = "120SecondsDuration")]
    public class EndeavourController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EndeavorDto> _dataShaper;

        public int FromQuery { get; private set; }

        public EndeavourController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<EndeavorDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
    
        }
        /// <summary>
        /// Gets List of Endeavours
        /// </summary>
        /// <param name="endeavourParameters"></param>
        /// <returns>List of Endeavours</returns>
        [HttpGet(Name = "GetEndeavours"), Authorize(Roles ="Manager")]
        [HttpHead]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetEndeavors([FromQuery] EndeavourParameters endeavourParameters)
        {
            if (!endeavourParameters.ValidGoalRange)
            {
                return BadRequest("Max goal can't be less than min goal");
            }
            var endeavourFromDb = await _repository.Endeavour.GetAllEndeavoursAsync(endeavourParameters, trackChanges: false);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(endeavourFromDb.MetaData));
            var endeavourDto = _mapper.Map<IEnumerable<EndeavorDto>>(endeavourFromDb);
           
            return Ok(_dataShaper.ShapeData(endeavourDto, endeavourParameters.Fields));
        }
        /// <summary>
        /// Gets an Endeavour by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns endeavour if Id exists</returns>
        [HttpGet("{id}", Name = "EndeavourById")]
        [ResponseCache(Duration = 60)]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> GetEndeavor(Guid id)
        {
            var endeavour = await _repository.Endeavour.GetEndeavourAsync(id, trackChanges: false);
            if (endeavour == null)
            {
                _logger.LogInfo($"Endeavour with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var endeavourDto = _mapper.Map<EndeavorDto>(endeavour);
                return Ok(endeavourDto);
            }
        }


        [HttpPost(Name = "CreateEndeavour")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEndeavour([FromBody] EndeavourForCreationDto endeavour)
        {
            var endeavourEntity = _mapper.Map<Endeavour>(endeavour);
            _repository.Endeavour.CreateEndeavour(endeavourEntity);
            await _repository.SaveAsync();

            var endeavourToReturn = _mapper.Map<EndeavorDto>(endeavourEntity);
            return CreatedAtRoute("EndeavourById", new { id = endeavourToReturn.Id }, endeavourToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEndeavourExistsAttribute))]
        public async Task<IActionResult> DeleteEndeavour(Guid id)
        {
            var endeavour = await _repository.Endeavour.GetEndeavourAsync(id, trackChanges: false);
            _repository.Endeavour.DeleteEndeavour(endeavour);
            await _repository.SaveAsync();
            return NoContent();
        }


        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEndeavourExistsAttribute))]

        public async Task<IActionResult> UpdateEndeavour(Guid id, [FromBody] EndeavorForUpdateDto endeavor)
        {
            var endeavourEntitiy = await _repository.Endeavour.GetEndeavourAsync(id, trackChanges: false);
            _mapper.Map(endeavor, endeavourEntitiy);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]

        public async Task<IActionResult> PartiallyUpdateEndeavour(Guid id, [FromBody] JsonPatchDocument<EndeavorForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client null.");
                return BadRequest("patchDoc object is null");
            }
            var endevourEntity = HttpContext.Items["endevour"] as Endeavour;
            var endevourToPatch = _mapper.Map<EndeavorForUpdateDto>(endevourEntity);
            patchDoc.ApplyTo(endevourToPatch, ModelState);
            TryValidateModel(endevourToPatch);
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }
            _mapper.Map(endevourToPatch, endevourEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetEndeavorsOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

    }
}