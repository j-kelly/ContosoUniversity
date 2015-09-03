namespace Factories
{
    using ContosoUniversity.Models;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System.Linq;

    public class DepartmentDetailFactoryQuery : FactoryQuery<DepartmentDetail>
    {
        public override IQueryable<object> Query(IQueryRepository repository, object additionalQueryData)
        {
            var departments = repository.GetEntities<Department>()
               .Select(dept => new DepartmentDetail
               {
                   DepartmentID = dept.DepartmentID,
                   Administrator = dept.Administrator != null ? dept.Administrator.LastName : null,
                   Budget = dept.Budget,
                   Name = dept.Name,
                   StartDate = dept.StartDate,
                   RowVersion = dept.RowVersion,
               });

            return departments;
        }
    }
}
