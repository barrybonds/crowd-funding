using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CrowdFunding.Controllers
{
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        public RootController(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }
        [HttpGet(Name = "GetRoot")]
        public IActionResult GetRoot([FromHeader(Name = "Accept")] string mediaType)
        {
            if (mediaType.Contains("application/vnd.crowdfunding.apiroot"))
            {

                var list = new List<Link> {
                new Link
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new { }),
                    Rel = "self",
                    Method = "GET"
                },
                new Link
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, "GetEndeavours", new { }),
                    Rel = "endeavors",
                    Method = "GET"
                },
                new Link
                {
                    Href = _linkGenerator.GetUriByName(HttpContext, "CreateEndeavour", new { }),
                    Rel = "create_endeavour",
                    Method = "POST"
                }
             };
                return Ok(list);
            }
            return NoContent();
        }
    }
}