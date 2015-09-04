namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using Models;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class CourseDetail
    {
        public class CourseDetailFactoryQuery : FactoryQuery<CourseDetail>
        {
            public override IQueryable<object> Query<T>(IQueryRepository repository, object additionalQueryData)
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

        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}