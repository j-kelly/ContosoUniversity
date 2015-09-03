namespace ContosoUniversity.Domain.Core.Repository.Projections.Factories
{
    using NRepository.Samples.Core;
    using System.Collections.Generic;

    public class ContosoFactoryQueryInterceptor : FactoryQueryInterceptor
    {
        private static readonly IEnumerable<IFactoryQuery> AllQueryFactories = new IFactoryQuery[]
        {
            new CourseDetailFactoryQuery(),
            new EnrollmentDateGroupFactoryQuery()
        };

        public ContosoFactoryQueryInterceptor()
            : base(AllQueryFactories)
        {
        }
    }
}
