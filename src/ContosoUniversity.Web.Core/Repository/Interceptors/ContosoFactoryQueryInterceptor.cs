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
            new CourseDetail.CourseDetailFactoryQuery(),
            new EnrollmentDateGroup.EnrollmentDateGroupFactoryQuery(),
            new DepartmentDetail.DepartmentDetailFactoryQuery(),
            new StudentDetail.StudentDetailFactoryQuery(),

            // Filters
            new SoftDeleteFilterFactoryQuery(),
        };

        public ContosoFactoryQueryInterceptor()
            : base(AllQueryFactories)
        {
        }
    }
}
