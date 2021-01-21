using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Type = Entities.Models.Type;

namespace CrowdFunding
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Endeavour, EndeavorDto>();
            CreateMap<EndeavourForCreationDto, Endeavour>();
            CreateMap<EndeavorForUpdateDto, Endeavour>();
            CreateMap<EndeavorForUpdateDto, Endeavour>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryForCreationDto, Category>();

            CreateMap<Type, TypeDto>();
            CreateMap<TypeForCeationDto, Entities.Models.Type>();
            CreateMap<UserForRegistrationDto, User>();
           

        }

    }
}
