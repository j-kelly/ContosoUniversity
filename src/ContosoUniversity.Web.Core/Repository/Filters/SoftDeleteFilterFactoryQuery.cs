namespace ContosoUniversity.Web.Core.Repository.Projections.Factories
{
    using Domain.Core.Repository;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using Strategies;
    using System.Linq;

    public class SoftDeleteFilterFactoryQuery : FactoryQuery<ISoftDelete>
    {
        public SoftDeleteFilterFactoryQuery()
            : base(isReentrent: true)
        {
        }

        public override IQueryable<object> Query<T>(IQueryRepository repository, object additionalQueryData)
        {
            var query = ((IQueryable<ISoftDelete>)repository.GetEntities<T>(new FilterByNoneSoftDeletedQueryStrategy()));
            return query;
        }
    }
}
