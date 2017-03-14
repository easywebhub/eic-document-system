using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eic.webapi.Dtos
{
    public class SignInRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string IdSrvAccountId { get; set; }
        public bool SignInByIdSrv { get { return !string.IsNullOrEmpty(this.IdSrvAccountId); } set { } }

        public SignInRequestDto()
        {
        
        }
    }
}
