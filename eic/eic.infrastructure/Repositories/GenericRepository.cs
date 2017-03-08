using Couchbase.Core;
using Couchbase.N1QL;
using eic.core;
using eic.core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EwDocument
    {
        protected readonly IEicBucketProvider _bucketProvider;
        protected readonly IBucket _bucket;
        //protected readonly IBucketContext _context;

        public GenericRepository(IEicBucketProvider bucketProvider)
        {
            _bucketProvider = bucketProvider;
            _bucket = bucketProvider.GetBucket();
            //_context = context;
        }

        public IQueryResult<T> FindAll()
        {
            var request = QueryRequest.Create(string.Format("SELECT * FROM {0}", typeof(T).Name));
            request.ScanConsistency(ScanConsistency.RequestPlus);
            return _bucket.Query<T>(request);
            
            //return _context.Query<T>()
            //   .ScanConsistency(ScanConsistency.RequestPlus)   // waiting for the indexing to complete before it returns a response
            //   ;
        }

        public List<T> GetList(List<string> ids)
        {
            return FindAll().Where(x => ids.Contains(x.Id)).ToList();   // waiting for the indexing to complete before it returns a response
        }

        public T Get(string id)
        {
            return _bucket.Get<T>(key: id).Value;
            //return _bucket.Get<T>(string.Format("{0}::{1}", typeof(T).Name , key)).Value;
        }

        public void AddOrUpdate(T entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _bucket.Upsert<T>(entity.Id, entity);


            // if there is no ID, then assume this is a "new" person
            // and assign an ID
            //if (string.IsNullOrEmpty(entity.Id))
            //    entity.Id = Guid.NewGuid().ToString(); // string.Format("{0}::{1}", entity.Type, Guid.NewGuid());

            //_context.Save(entity);

            // alternate: with plain .NET SDK
            //            var doc = new Document<Person>
            //            {
            //                Id = "Person::" + person.Id,
            //                Content = person
            //            };
            //            _bucket.Upsert(doc);
        }

        public void Delete(string id)
        {
            // you could use _context.Remove(document); if you have the whole document
            //_bucket.Remove(string.Format("{0}::{1}", typeof(T).Name, id));
            _bucket.Remove(id);
        }

    }
}
