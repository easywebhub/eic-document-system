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
    public class ActionRepository : GenericRepository<core.Action>, IActionRepository
    {
        public ActionRepository(IEicBucketProvider bucketProvider) : base(bucketProvider)
        {
        }

        public core.Action GetByName(string name)
        {
            return FindAll().Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }
    }
}
