namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InstructorDetail
    {
        public int InstructorId { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Office Location")]
        public string OfficeLocation { get; set; }

        public IEnumerable<CourseDetail> CourseDetails { get; set; }
    }
}
