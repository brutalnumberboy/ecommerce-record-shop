using API.Models;
namespace API.Mapping.DTO
{
    public class BasketItemDTO
    {
        public int Amount {get; set;}
        public string AlbumId {get; set;}
        public string Title {get; set;}
        public string Artist {get; set;}
        public decimal Price {get; set;}
    }
}