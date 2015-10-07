namespace ContosoUniversity.Web.Mvc.Features.Instructor
{
    using Core;
    
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ModifyInstructorAndCoursesViewModel : CommandToViewModelBase<ContosoUniversity.Domain.Core.Behaviours.Instructors.InstructorModifyAndCourses.CommandModel>
    {
        public ModifyInstructorAndCoursesViewModel()
        {
        }

        public ModifyInstructorAndCoursesViewModel(ContosoUniversity.Domain.Core.Behaviours.Instructors.InstructorModifyAndCourses.CommandModel commandModel)
            : base(commandModel)
        {
        }

        public int InstructorId
        {
            get { return CommandModel.InstructorId; }
            set { CommandModel.InstructorId = value; }
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

        public IEnumerable<int> SelectedCourses
        {
            get { return CommandModel.SelectedCourses; }
            set { CommandModel.SelectedCourses = value; }
        }
    }
}
