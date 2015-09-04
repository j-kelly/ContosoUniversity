namespace ContosoUniversity.Web.Core.Repository.Strategies
{
    using ContosoUniversity.Core;
    using Domain.Core.Repository;
    using NRepository.Core.Query;
    using System.Linq;

    public class FilterByNoneSoftDeletedQueryStrategy : QueryStrategy
    {
        public override IQueryable<T> GetQueryableEntities<T>(object additionalQueryData)
        {
            if (!typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
                throw new ContosoUniversityException("Only types derived from ISoftDelete can be used with this strategy: " + GetType().Name);

            var query = QueryableRepository.GetQueryableEntities<T>(additionalQueryData);
            var filteredQuery = ((IQueryable<ISoftDelete>)query).Where(p => !p.IsDeleted).Cast<T>();

            return filteredQuery;
        }
    }
}
