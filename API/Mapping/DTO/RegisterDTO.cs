using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Mapping.DTO
{
    public class RegisterDTO
    {
        [Required]
        public string UserName {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}