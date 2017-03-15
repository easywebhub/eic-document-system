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
        public string IdSrvAccountId { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //public string Email { get; set; }

        public AccountInfo Info { get; set; }

        public AddAccountDto() { }
    }

    public class CreateAccountDto: AddAccountDto
    {
        public List<AccountInGroup> Groups { get; set; }
        public List<ActionOfAccount> Actions { get; set; }
    }
    
      

}
