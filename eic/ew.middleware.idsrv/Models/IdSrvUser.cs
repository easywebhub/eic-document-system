using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.middleware.idsrv_wrapper.Models
{
    public class IdSrvUser
    {
        #region properties
        public string UserId { get; private set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        #endregion
    }

    public class IdSrvUserDetail
    {
        #region properties
        public string UserId { get; private set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }
        public List<UserClaim> UserClaims { get; set; }
        #endregion
    }

    public class UserClaim
    {
        public UserClaim() { }

        public int Id { get; set; }
        public string Type { get; set; }
    }
}
