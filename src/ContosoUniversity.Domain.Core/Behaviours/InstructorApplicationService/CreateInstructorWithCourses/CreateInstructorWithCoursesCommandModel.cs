namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateInstructorWithCoursesCommandModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; } = DateTimeHelper.Today;

        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string OfficeLocation { get; set; }

        public int[] SelectedCourses { get; set; }
    }
}
