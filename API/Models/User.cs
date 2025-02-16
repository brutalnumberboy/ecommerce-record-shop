using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace API.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int Id {get; set;}
        [Required]
        public string UserName {get; set;}
        [Required]
        public string Email {get; set;}
    }
}