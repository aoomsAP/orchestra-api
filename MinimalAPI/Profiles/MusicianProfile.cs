using AutoMapper;
using MinimalAPI.Models;
using Project.Entities;

namespace MinimalAPI.Profiles
{
    public class MusicianProfile : Profile
    {
        public MusicianProfile() 
        {
            CreateMap<Musician, MusicianDto>();
            CreateMap<MusicianCreationDto, Musician>();
        }
    }
}
