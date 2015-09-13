namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using Cache;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using NRepository.EntityFramework;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class EnrollmentDateGroup : ICacheableProjection
    {
        public class FactoryQuery : FactoryQuery<EnrollmentDateGroup>
        {
            public override IQueryable<object> Query<T>(IQueryRepository repository, object additionalQueryData)
            {
                // Return readonly data
                string sql = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
                   + "FROM Person "
                   + "WHERE Discriminator = 'Student' "
                   + "GROUP BY EnrollmentDate";

                var enrollmentDateGroups = repository.ExecuteSqlQuery<EnrollmentDateGroup>(sql);
                return enrollmentDateGroups.AsQueryable();
            }
        }

        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}