using Couchbase.N1QL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core.Repositories
{
    public interface IGenericRepository<T> where T : EwDocument
    {
        void AddOrUpdate(T entity);
        void Delete(string id);
        IQueryResult<T> FindAll();
        List<T> GetList(List<string> ids);
        T Get(string id);
    }
}
