namespace ContosoUniversity.Web.Mvc.Features.Instructor.ViewModels
{
    using ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService;
    using Core;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateInstructorWithCoursesViewModel : CommandToViewModelBase<CreateInstructorWithCourses.CommandModel>
    {
        public CreateInstructorWithCoursesViewModel()
        {
        }

        public CreateInstructorWithCoursesViewModel(CreateInstructorWithCourses.CommandModel commandModel)
            : base(commandModel)
        {
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