namespace ContosoUniversity.Web.App.Features.Instructor.ViewModels
{
    using ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateInstructorWithCoursesViewModel
    {
        public CreateInstructorWithCoursesViewModel()
        {
            CommandModel = new CreateInstructorWithCourses.CommandModel();
        }

        public CreateInstructorWithCoursesViewModel(CreateInstructorWithCourses.CommandModel commandModel)
        {
            CommandModel = commandModel;
        }

        public CreateInstructorWithCourses.CommandModel CommandModel
        {
            get;
        }

        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return CommandModel.LastName; }
            set { CommandModel.LastName = value; }
        }

        [Display(Name = "First Name")]
        public string FirstMidName
        {
            get { return CommandModel.FirstMidName; }
            set { CommandModel.FirstMidName = value; }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate
        {
            get { return CommandModel.HireDate; }
            set { CommandModel.HireDate = value; }
        }

        [Display(Name = "Office Location")]
        public string OfficeLocation
        {
            get { return CommandModel.OfficeLocation; }
            set { CommandModel.OfficeLocation = value; }
        }

        public int[] SelectedCourses
        {
            get { return CommandModel.SelectedCourses; }
            set { CommandModel.SelectedCourses = value; }
        }
    }
}