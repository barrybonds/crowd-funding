using AutoMapper;
//using CompanyEmployees.ActionFilters;
//using CompanyEmployees.ModelBinders;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
//using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Entities.Models.Type;

namespace CrowdFunding.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/role")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public TypeController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEndeavourRoleTypeAsync()
        {
            var roles = await _repository.Type.GetAllTypeAsync(trackChanges: false);
            var roleDto = _mapper.Map<IEnumerable<TypeDto>>(roles);
            return Ok(roleDto);
        }


        [HttpGet("{id}", Name = "RoleById")]
        public async Task<IActionResult> GetEndeavor(Guid id)
        {
            var role = await _repository.Type.GetTypeAsync(id, trackChanges: false);
            if (role == null)
            {
                _logger.LogInfo($"Role with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var RoleDto = _mapper.Map<TypeDto>(role);
                return Ok(RoleDto);
            }
        }


        [HttpPost(Name = "CreateEndeavourRoleType")]
        public async Task<IActionResult> CreateEndeavourRoleType([FromBody] TypeForCeationDto role)
        {
            var roleEntity = _mapper.Map<Type>(role);
            _repository.Type.CreateType(roleEntity);
            await _repository.SaveAsync();

            var roleEntityToReturn = _mapper.Map<TypeDto>(roleEntity);
            return CreatedAtRoute("RoleById", new { id = roleEntityToReturn.Id }, roleEntityToReturn);
        }

        [HttpDelete("{id}")]      
        public async Task<IActionResult> DeleteType(Guid id)
        {
            var type = await _repository.Type.GetTypeAsync(id, trackChanges: false);
            _repository.Type.DeleteType(type);
            await _repository.SaveAsync();
            return NoContent();
        }

    }
}