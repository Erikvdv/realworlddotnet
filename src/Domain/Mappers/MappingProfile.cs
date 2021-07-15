using AutoMapper;
using realworlddotnet.Domain.Dto;
using realworlddotnet.Domain.Entities;

namespace realworlddotnet.Domain.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}