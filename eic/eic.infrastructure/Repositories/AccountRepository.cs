using Couchbase.Core;
using Couchbase.N1QL;
using eic.core;
using eic.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eic.infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(IEicBucketProvider _bucketProvider) : base(_bucketProvider)
        {
        }
        
        public IQueryable<Account> Find(EntityQueryParams queryParams)
        {
            var sql = FindAll();

            return sql.Skip(queryParams.Offset).Take(queryParams.Limit).AsQueryable();
        }

        public Account GetByUsername(string username)
        {
            return FindAll().Where(x => x.UserName == username).FirstOrDefault();
        }

        public bool IsExitsUserName(string username)
        {
            return this.FindAll().Any(x => x.UserName == username);
        }

        public bool IsExitsEmail(string email)
        {
            return this.FindAll().Any(x => x.Info != null && x.Info.Email == email);
        }

        public bool IsIdentityEmail(string username, string email)
        {
            return this.FindAll().Any(x => x.UserName != username && x.Info != null && x.Info.Email == email);
        }
        
       
    }
}
