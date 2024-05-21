using AutoMapper;
using MinimalAPI.Models;
using Project.Entities;

namespace MinimalAPI.Profiles
{
    public class OrchestraProfile : Profile
    {
        public OrchestraProfile() 
        {
            CreateMap<Orchestra, OrchestraDto>()
                .ForMember(
                    destination => destination.Country,
                    option => option.MapFrom(x => x.Country.Name)
                );
            CreateMap<OrchestraCreationDto, Orchestra>();
        }
    }
}
