namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using Cache;
    using NRepository.Core.Query.Interceptors.Factories;
    using Projections;
    using Projections.Factories;
    using System.Collections.Generic;

    public class ContosoFactoryQueryInterceptor : FactoryQueryInterceptor
    {
        private static readonly IEnumerable<IFactoryQuery> AllQueryFactories = new IFactoryQuery[]
        {
            // Caching
            new EfCacheableFactoryQuery(),
            new CacheableProjectionFactoryQuery(),

            // Projections
            new CourseDetail.FactoryQuery(),
            new EnrollmentDateGroup.FactoryQuery(),
            new DepartmentDetail.FactoryQuery(),
            new StudentDetail.FactoryQuery(),

            // Filters
            new SoftDeleteFilterFactoryQuery(),
        };

        public ContosoFactoryQueryInterceptor()
            : base(AllQueryFactories)
        {
        }
    }
}
