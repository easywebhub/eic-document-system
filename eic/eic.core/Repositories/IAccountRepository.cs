using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        IQueryable<Account> Find(EntityQueryParams queryParams);
        bool IsExitsUserName(string username);
        Account GetByUsername(string username);
        bool IsExitsEmail(string email);
        bool IsIdentityEmail(string username, string email);
    }
}
