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
        public string UserName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
    }
}