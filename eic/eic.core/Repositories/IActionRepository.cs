using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core.Repositories
{
    public interface IActionRepository : IGenericRepository<Action>
    {
        Action GetByName(string name);
    }
}
