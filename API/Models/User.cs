using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace API.Models
{
    public class User : IdentityUser
    {
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z0-9]+$")]
        public override string UserName {get; set;}
        [Required]
        [EmailAddress]
        public override string Email {get; set;}
        public string? Token {get; set;}
    }
}