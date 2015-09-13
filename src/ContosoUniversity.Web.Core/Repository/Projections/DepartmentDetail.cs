namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class DepartmentDetail
    {
        public class FactoryQuery : FactoryQuery<DepartmentDetail>
        {
            public override IQueryable<object> Query<T>(IQueryRepository repository, object additionalQueryData)
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

        public int DepartmentID { get; set; }

        public string Administrator { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        public byte[] RowVersion { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
    }
}
