

namespace API.Mapping.DTO
{
    public class UserBasketDTO
    {
        public required List<BasketItemDTO> BasketItems { get; set; }
        public int TotalPrice { get; set; }
        public required string ShippingAddress {get; set;}
        public int ShippingPrice {get; set;}
    }
}