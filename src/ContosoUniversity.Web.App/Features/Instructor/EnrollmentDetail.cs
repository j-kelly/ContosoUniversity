namespace ContosoUniversity.Features.Instructor
{
    using Models;

    public class EnrollmentDetail
    {
        public string LastName { get; set; }

        public string FirstMidName { get; set; }

        public string FullName => $"{LastName}, {FirstMidName}";

        public Grade? Grade { get; set; }
    }
}
