namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using ContosoUniversity.Models;
    using System.ComponentModel.DataAnnotations;

    // TODO - move to internal setters
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}