using AutoMapper;
using MinimalAPI.Models;
using Project.Entities;

namespace MinimalAPI.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<CountryCreationDto, Country>();
        }
    }
}
