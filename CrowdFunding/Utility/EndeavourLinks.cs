//using Contracts;
//using Entities.DataTransferObjects;
//using Entities.LinkModels;
//using Entities.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing;
//using Microsoft.Net.Http.Headers;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CrowdFunding.Utility
//{
//    public class EndeavourLinks
//    {
//        private readonly LinkGenerator _linkGenerator;
//        private readonly IDataShaper<EndeavorDto> _dataShaper;
//        public EndeavourLinks(LinkGenerator linkGenerator, IDataShaper<EndeavorDto>
//        dataShaper)
//        {
//            _linkGenerator = linkGenerator;
//            _dataShaper = dataShaper;
//        }

//        public LinkResponse TryGenerateLinks(IEnumerable<EndeavorDto> endeavourtDto, string fields, HttpContext httpContext)
//        {
//            var shapedEndeavours = ShapeData(endeavourtDto, fields);

//            if (ShouldGenerateLinks(httpContext))
//                return ReturnLinkdedEndeavours(endeavourtDto, fields, httpContext, shapedEndeavours);

//            return ReturnShapedEndeavour(shapedEndeavours);
//        }

//        private LinkResponse ReturnLinkdedEndeavours(IEnumerable<EndeavorDto> endeavourDto, string fields, Guid endeavorId, HttpContext httpContext, List<Entity> shapedEndeavours)
//        {
//            var endeavourDtoList = endeavourDto.ToList();

//            for (var index = 0; index < endeavourDtoList.Count(); index++)
//            {
//                var endeavourLinks = CreateLinksForEndeavour(httpContext, companyId, endeavourDtoList[index].Id, fields);
//                shapedEndeavours[index].Add("Links", endeavourLinks);
//            }

//            var endeavourCollection = new LinkCollectionWrapper<Entity>(shapedEndeavours);
//            var linkedEndeavours = CreateLinksForEndeavours(httpContext, endeavourCollection);

//            return new LinkResponse { HasLinks = true, LinkedEntities = linkedEndeavours };
//        }

//        private bool ShouldGenerateLinks(HttpContext httpContext)
//        {
//            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

//            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
//        }

//        private LinkResponse ReturnShapedEndeavour(object shapedEndeavours)
//        {
//            throw new NotImplementedException();
//        }

//        private List<Entity> ShapeData(IEnumerable<EndeavorDto> endeavourtDto, string fields)
//        =>
//            _dataShaper.ShapeData(endeavourtDto, fields)
//               .Select(e => e.Entity)
//               .ToList();


//        private List<Link> CreateLinksForEndeavour(HttpContext httpContext, Guid companyId, Guid id, string fields = "")
//        {
//            var links = new List<Link>
//            {
//                new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployeeForCompany", values: new { companyId, id, fields }), "self","GET"),
//                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteEmployeeForCompany", values: new { companyId, id }), "delete_employee", "DELETE"),
//                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateEmployeeForCompany", values: new { companyId, id }), "update_employee", "PUT"),
//                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdateEmployeeForCompany", values: new { companyId, id }),"partially_update_employee","PATCH")
//            };
//            return links;
//        }

//        private LinkCollectionWrapper<Entity> CreateLinksForEndeavours(HttpContext httpContext, LinkCollectionWrapper<Entity> endeavoursWrapper)
//        {
//            endeavoursWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployeesForCompany", values: new { }), "self", "GET"));
//            return endeavoursWrapper;
//        }
//    }
//}
