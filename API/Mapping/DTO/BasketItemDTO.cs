namespace API.Mapping.DTO
{
    public class BasketItemDTO
    {
        public required int Id {get; set;}
        public required int Amount {get; set;}
        public required string Title {get; set;}
        public required int Price {get; set;}
        public required int AlbumId {get; set;}
    }
}