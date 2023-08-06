using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.Users.Core.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required, MinLength(6)]
        public string NewPassword { get; set; }
        [Required, MinLength(6), Compare("NewPassword")]
        public string ConfirmPassword { get; set; }

        public bool IsReset { get; set; }
        public List<string> Messages { get; set; } = new();
    }
}
