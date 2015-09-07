namespace ContosoUniversity.Web.App.Features.Instructor
{
    using Domain.Core.Behaviours.InstructorApplicationService;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ModifyInstructorAndCoursesViewModel
    {
        public ModifyInstructorAndCoursesViewModel(ModifyInstructorAndCourses.CommandModel commandModel)
        {
            CommandModel = commandModel;
        }

        public ModifyInstructorAndCoursesViewModel()
        {
            CommandModel = new ModifyInstructorAndCourses.CommandModel();
        }

        public ModifyInstructorAndCourses.CommandModel CommandModel
        {
            get;
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
