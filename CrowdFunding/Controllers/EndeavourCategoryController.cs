using AutoMapper;
//using CompanyEmployees.ActionFilters;
//using CompanyEmployees.ModelBinders;
using Contracts;
using CrowdFunding.ModelBinders;
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

namespace CrowdFunding.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/category")]
    [ApiController]
    public class EndeavourCategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EndeavourCategoryController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _repository.Category.GetAllCategoriesAsync(trackChanges: false);
            var catsDto = _mapper.Map<IEnumerable<CategoryDto>>(cats);
            return Ok(catsDto);
        }


        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Ok(categoryDto);
            }
        }

        [HttpGet("collection/({ids})", Name = "CategoryCollection")]
        public IActionResult GetCategoryCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameters ids is null");
                return BadRequest("Parameter ids is null");
            }
            var categoryEntities = _repository.Category.GetByIds(ids, trackChanges: false);
            if (ids.Count() != categoryEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
            return Ok(categoriesToReturn);
        }


        [HttpPost("collection")]
        public IActionResult CreateCategoryCollection([FromBody] IEnumerable<CategoryForCreationDto> categoryCollection)
        {
            if (categoryCollection == null)
            {
                _logger.LogError("Category collection sent from client is null.");
                return BadRequest("Company collection is null");
            }
            var categoryEntities = _mapper.Map<IEnumerable<Category>>(categoryCollection);
            foreach (var category in categoryEntities)
            {
                _repository.Category.CreateCategory(category);
            }
            _repository.SaveAsync();
            var categoryCollectionToReturn =
            _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
            var ids = string.Join(",", categoryCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("CategoryCollection", new { ids },
            categoryCollectionToReturn);
        }


        [HttpPost(Name = "CreateEndeavourCategory")]
        public async Task<IActionResult> CreateEndeavourCategory([FromBody] CategoryForCreationDto endeavourCategory)
        {
            var categoryEntity = _mapper.Map<Category>(endeavourCategory);
            _repository.Category.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            var categoryEntityToReturn = _mapper.Map<CategoryDto>(categoryEntity);
            return CreatedAtRoute("CategoryById", new { id = categoryEntityToReturn.Id }, categoryEntityToReturn);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);
            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetCategoriesOptions()
        {
            Response.Headers.Add("Allow", "GET, OPTIONS, POST");
            return Ok();
        }

    }
}