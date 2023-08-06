using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Core.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
        public string Phone { get; set; }

        public List<string> Roles { get; set; } = new();
        //Response
        public bool IsRegistered { get; set; }
        public List<string> Messages { get; set; } = new();
    }
}
