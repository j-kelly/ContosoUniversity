namespace ContosoUniversity.Web.App.Features.Instructor
{
    using ContosoUniversity.Web.Core.Repository.Projections;
    using System.Collections.Generic;

    public class InstructorIndexData
    {
        public IEnumerable<InstructorDetail> Instructors { get; set; }
        public IEnumerable<CourseDetail> Courses { get; set; }
        public IEnumerable<EnrollmentDetail> Enrollments { get; set; }
    }
}