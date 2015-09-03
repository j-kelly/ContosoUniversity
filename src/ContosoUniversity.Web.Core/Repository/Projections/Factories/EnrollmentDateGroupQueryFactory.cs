namespace Factories
{
    using ContosoUniversity.Web.Core.Repository.Projections;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using NRepository.EntityFramework;
    using System.Linq;

    public class EnrollmentDateGroupFactoryQuery : FactoryQuery<EnrollmentDateGroup>
    {
        public override IQueryable<object> Query(IQueryRepository repository, object additionalQueryData)
        {
            // Return readonly data
            string sql = "SELECT EnrollmentDate, COUNT(*) AS StudentCount "
               + "FROM Person "
               + "WHERE Discriminator = 'Student' "
               + "GROUP BY EnrollmentDate";

            var enrollmentDateGroups = repository.ExecuteSqlQuery<EnrollmentDateGroup>(sql);
            return (IQueryable<object>)enrollmentDateGroups.AsQueryable();
        }
    }
}
