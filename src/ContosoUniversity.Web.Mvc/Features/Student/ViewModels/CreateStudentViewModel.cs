namespace ContosoUniversity.Web.Mvc.Features.Student.ViewModels
{
    using Core;
    
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateStudentViewModel : CommandToViewModelBase<ContosoUniversity.Domain.Core.Behaviours.Students.StudentCreate.CommandModel>
    {
        public CreateStudentViewModel()
        {
        }

        public CreateStudentViewModel(ContosoUniversity.Domain.Core.Behaviours.Students.StudentCreate.CommandModel commandModel)
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