namespace Factories
{
    using ContosoUniversity.Models;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System.Linq;

    public class CourseDetailFactoryQuery : FactoryQuery<CourseDetail>
    {
        public override IQueryable<object> Query(IQueryRepository repository, object additionalQueryData)
        {
            var courses = repository.GetEntities<Course>()
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