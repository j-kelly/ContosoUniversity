namespace ContosoUniversity.Web.Mvc.Features.Instructor
{
    using ContosoUniversity.Web.Core.Repository.Projections;
    using System.Collections.Generic;

    public class InstructorIndexDataViewModel
    {
        public IEnumerable<InstructorDetailViewModel> Instructors { get; set; }
        public IEnumerable<CourseDetail> Courses { get; set; }
        public IEnumerable<EnrollmentDetailViewModel> Enrollments { get; set; }
    }
}