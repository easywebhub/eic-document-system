using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core.Repositories
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        Group GetByName(string name);
    }
}
