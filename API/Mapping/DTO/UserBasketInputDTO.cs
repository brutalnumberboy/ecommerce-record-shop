
namespace API.Mapping.DTO
{
    public class UserBasketInputDTO
    {
        public List<BasketItemInputDTO> BasketItems { get; set; }
        public string ShippingAddress {get; set;}
        public decimal ShippingPrice {get; set;}
    }
}