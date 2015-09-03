namespace ContosoUniversity.Web.Core.Repository.Interceptors
{
    using Factories;
    using NRepository.Core.Query.Interceptors.Factories;
    using Projections.Factories;
    using System.Collections.Generic;

    public class ContosoFactoryQueryInterceptor : FactoryQueryInterceptor
    {
        private static readonly IEnumerable<IFactoryQuery> AllQueryFactories = new IFactoryQuery[]
        {
            new CourseDetailFactoryQuery(),
            new EnrollmentDateGroupFactoryQuery(),
            new InstructorDetailFactoryQuery(),
            new DepartmentDetailFactoryQuery(),
            new StudentDetailFactoryQuery()
        };

        public ContosoFactoryQueryInterceptor()
            : base(AllQueryFactories)
        {
        }
    }
}
