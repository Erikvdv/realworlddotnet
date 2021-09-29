using AutoMapper;
using realworlddotnet.Core.Dto;
using realworlddotnet.Core.Entities;

namespace realworlddotnet.Core.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}