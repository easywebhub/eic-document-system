using eic.core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eic.application.Entities.Dto
{
    public class AddAccountDto
    {
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public AccountInfo Info { get; set; }

        public AddAccountDto() { }
    }

   

}
