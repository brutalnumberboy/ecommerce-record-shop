using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using API.Mapping.DTO;

namespace API.Models
{
    public class UserBasket
    {
        [Key]
        public int Id { get; set; }
        public  List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public decimal TotalPrice { get; set; }
        public  string ShippingAddress {get; set;}
        public decimal ShippingPrice {get; set;}
        public string UserId {get; set;}
        public User? User { get; set; }
    
    }
}