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
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(IEicBucketProvider bucketProvider) : base(bucketProvider)
        {
        }

        public Group GetByName(string name)
        {
            return FindAll().Where(x => x.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }
    }
}
