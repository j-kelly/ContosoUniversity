namespace ContosoUniversity.Web.Mvc.Features.Student.ViewModels
{
    using Core;
    using Domain.Core.Behaviours.StudentApplicationService;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateStudentViewModel : CommandToViewModelBase<CreateStudent.CommandModel>
    {
        public CreateStudentViewModel()
        {
        }

        public CreateStudentViewModel(CreateStudent.CommandModel commandModel)
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
        [Display(Name = "Enrollment Date")]
        public DateTime EnrollmentDate
        {
            get { return CommandModel.EnrollmentDate; }
            set { CommandModel.EnrollmentDate = value; }
        }
    }
}