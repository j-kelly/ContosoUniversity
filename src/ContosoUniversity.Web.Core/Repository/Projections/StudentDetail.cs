namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using ContosoUniversity.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StudentDetail
    {
        public class EnrollmentDetail
        {
            public string CourseTitle { get; set; }
            public Grade? Grade { get; set; }
        }

        public int StudentId { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Enrollment Details")]
        public IEnumerable<EnrollmentDetail> EnrollmentDetails { get; set; }
    }
}
