namespace ContosoUniversity.Web.Mvc.Features.Instructor
{
    using Models;

    public class EnrollmentDetailViewModel
    {
        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public string FullName => $"{LastName}, {FirstMidName}";

        public Grade? Grade { get; set; }
    }
}
