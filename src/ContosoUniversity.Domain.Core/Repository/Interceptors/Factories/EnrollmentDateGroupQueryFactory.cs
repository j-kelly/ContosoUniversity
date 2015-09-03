namespace ContosoUniversity.Domain.Core.Repository.Projections.Factories
{
    using System.Linq;
    using NRepository.Core.Query;
    using NRepository.EntityFramework;
    using NRepository.Samples.Core;
    using ViewModels;

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
