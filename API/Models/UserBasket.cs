using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mapping.DTO;

namespace API.Models
{
    public class UserBasket
    {
        public required List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public int TotalPrice { get; set; }
        public required string ShippingAddress {get; set;}
        public int ShippingPrice {get; set;}
    }
}