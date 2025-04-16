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
        public int Id { get; set; }
        public  int Amount {get; set;}
        public string AlbumId {get; set;}
        public Album? Album { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}