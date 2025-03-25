using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BasketItem
    {
        public required int Id {get; set;}
        public required int Amount {get; set;}
        public required string Title {get; set;}
        public required int Price {get; set;}
        public required int AlbumId {get; set;}
    }
}