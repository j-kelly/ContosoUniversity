namespace ContosoUniversity.Web.Core.Repository.Cache
{
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System.Linq;
    using System.Runtime.Caching;

    /// <summary>
    /// Used to cache projections
    /// </summary>
    public class CacheableProjectionFactoryQuery : FactoryQuery<ICacheableProjection>
    {
        private static readonly object SyncObject = new object();
        private static readonly MemoryCache Cache = MemoryCache.Default;

        public CacheableProjectionFactoryQuery()
            : base(isReentrent: true)
        {
        }

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
                    items = repository.GetEntities<T2>(this).ToArray().AsQueryable();
                    Cache.Add(key, items, new CacheItemPolicy());
                }

                return (IQueryable<object>)items;
            }
        }
    }
}
