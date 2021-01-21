using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrowdFunding.ActionFiltersz
{
    public class ValidateEndeavourExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateEndeavourExistsAttribute(IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT") ? true : false;
            var id = (Guid)context.ActionArguments["id"];
            var endeavour = await _repository.Endeavour.GetEndeavourAsync(id, trackChanges);
            if (endeavour == null)
            {
                _logger.LogInfo($"Endeavour with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("Endeavour", endeavour);
                await next();
            }
        }
    }
}
