namespace ContosoUniversity.Domain.Core.Repository.Projections.Factories
{
    using System.Linq;
    using Models;
    using NRepository.Core.Query;
    using NRepository.Samples.Core;
    using Services.QueryServices;

    public class CourseDetailFactoryQuery : FactoryQuery<CourseDetail>
    {
        public override IQueryable<object> Query(IQueryRepository repository, object additionalQueryData)
        {
            var courses = repository.GetEntities<Course>(
               new OrderByQueryStrategy<Course>(p => p.CourseID))
               .Select(course => new CourseDetail
               {
                   CourseID = course.CourseID,
                   Credits = course.Credits,
                   DepartmentName = course.Department.Name,
                   Title = course.Title,
                   DepartmentID = course.DepartmentID
               });

            return courses;
        }
    }
}
