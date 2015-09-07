namespace ContosoUniversity.Web.App.Features.Student.ViewModels
{
    using Domain.Core.Behaviours.StudentApplicationService;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateStudentViewModel
    {
        public CreateStudentViewModel(CreateStudent.CommandModel commandModel)
        {
            CommandModel = commandModel;
        }

        public CreateStudentViewModel()
        {
            CommandModel = new CreateStudent.CommandModel();
        }

        public CreateStudent.CommandModel CommandModel
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
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate
        {
            get { return CommandModel.EnrollmentDate; }
            set { CommandModel.EnrollmentDate = value; }
        }
    }
}