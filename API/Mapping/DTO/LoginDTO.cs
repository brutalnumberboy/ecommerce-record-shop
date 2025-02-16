using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.Mapping.DTO
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}