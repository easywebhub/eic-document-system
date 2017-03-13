using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.middleware.idsrv_wrapper.Models
{
    public class RegisterUserDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        
    }

    public class SignInDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
