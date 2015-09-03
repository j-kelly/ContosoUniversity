namespace Factories
{
    using ContosoUniversity.Models;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System.Linq;

    public class InstructorDetailFactoryQuery : FactoryQuery<InstructorDetail>
    {
        public override IQueryable<object> Query(IQueryRepository repository, object additionalQueryData)
        {
            var instructors = repository.GetEntities<Instructor>()
                .Select(p => new InstructorDetail
                {
                    FirstMidName = p.FirstMidName,
                    HireDate = p.HireDate,
                    InstructorId = p.ID,
                    LastName = p.LastName,
                    OfficeLocation = p.OfficeAssignment.Location,
                    CourseDetails = p.Courses.Select(p1 => new CourseDetail
                    {
                        CourseID = p1.CourseID,
                        Credits = p1.Credits,
                        DepartmentID = p1.DepartmentID,
                        DepartmentName = p1.Department.Name,
                        Title = p1.Title
                    }),
                });

            return instructors;
        }
    }
}
