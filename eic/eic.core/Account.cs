using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core
{
    //[DocumentTypeFilter("Account")]
    public class Account: EwDocument
    {
        public Account() : base("Account")
        {
            Info = new AccountInfo();
        }
        public string IdSrvAccountId { get; set; }
        public string AccountType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Status { get; set; }
        public AccountInfo Info { get; set; }
    }

    public class AccountInfo
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
    }
}
