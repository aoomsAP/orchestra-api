using AutoMapper;
using MinimalAPI.Models;
using Project.Entities;

namespace MinimalAPI.Profiles
{
    public class OrchestraProfile : Profile
    {
        public OrchestraProfile() 
        { 
            CreateMap<Orchestra, OrchestraDto>();
        }
    }
}
