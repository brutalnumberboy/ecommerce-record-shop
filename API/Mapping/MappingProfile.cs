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
            CreateMap <UserBasket, UserBasketDTO>().ReverseMap();

            CreateMap<BasketItem, BasketItemDTO>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Album.Title))
                .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Album.Artist))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Album.Price));

            CreateMap<UserBasket, UserBasketDTO>();

            CreateMap<BasketItemInputDTO, BasketItem>();
            CreateMap<UserBasketInputDTO, UserBasket>();
        }
    }
}