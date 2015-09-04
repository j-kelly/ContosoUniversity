namespace ContosoUniversity.Web.Core.Repository.Cache
{
    using Domain.Core.Repository;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.Caching;

    /// <summary>
    /// Used to cache entities found in the database
    /// </summary>
    public class EfCacheableFactoryQuery : FactoryQuery<ICacheable>
    {
        private static readonly object SyncObject = new object();
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public override IQueryable<object> Query<T2>(IQueryRepository repository, object additionalQueryData)
        {
            var key = typeof(T2).AssemblyQualifiedName;
            var items = Cache.Get(key);

            // Is it already cached?
            if (items != null)
                return (IQueryable<T2>)items;

            lock (SyncObject)
            {
                items = Cache.Get(key);
                if (items == null)
                {
                    // if your not generating proxies you can use the following :
                    // items = repository.GetEntities<T>(new AsNoTrackingQueryStrategy(),this).ToArray().AsQueryable();

                    // Required so you don't put proxied entities into the cache (
                    var dbContext = (DbContext)Activator.CreateInstance(repository.ObjectContext.GetType());
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    items = dbContext.Set<T2>().AsNoTracking().ToArray().AsQueryable();

                    Cache.Add(key, items, new CacheItemPolicy());
                }

                return (IQueryable<object>)items;
            }
        }
    }
}