
namespace API.Mapping.DTO
{
    public class UserBasketDTO
    {
        public List<BasketItemDTO> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
        public string ShippingAddress {get; set;}
        public decimal ShippingPrice {get; set;}
    }
}