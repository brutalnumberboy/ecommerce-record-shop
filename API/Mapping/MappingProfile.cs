using API.Mapping.DTO;
using API.Models;
using AutoMapper;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<Album, AlbumDTO>();
            CreateMap <RegisterDTO, User>();
            CreateMap <LoginDTO, User>();
            CreateMap <User, UserDTO>();
        }
    }
}