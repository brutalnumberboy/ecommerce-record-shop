using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class BasketItem
    {
        [Key]
        public int Id {get; set;}
        public required int Amount {get; set;}
        public required string Title {get; set;}
        public required int Price {get; set;}
        public required int AlbumId {get; set;}
    }
}