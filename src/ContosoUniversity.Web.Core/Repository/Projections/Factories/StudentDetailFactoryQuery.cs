namespace ContosoUniversity.Web.Core.Repository.Projections.Factories
{
    using ContosoUniversity.Models;
    using ContosoUniversity.Web.Core.Repository.Projections;
    using NRepository.Core.Query;
    using NRepository.Core.Query.Interceptors.Factories;
    using System.Linq;

    public class StudentDetailFactoryQuery : FactoryQuery<StudentDetail>
    {
        public override IQueryable<object> Query(IQueryRepository repository, object additionalQueryData)
        {
            var students = repository.GetEntities<Student>()
                .Select(student => new StudentDetail
                {
                    StudentId = student.ID,
                    EnrollmentDate = student.EnrollmentDate,
                    FirstMidName = student.FirstMidName,
                    LastName = student.LastName,
                    EnrollmentDetails = student.Enrollments.Select(enrollment => new StudentDetail.EnrollmentDetail
                    {
                        CourseTitle = enrollment.Course.Title,
                        Grade = enrollment.Grade
                    }),
                });

            return students;
        }
    }
}
