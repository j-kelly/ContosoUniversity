namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.CreateDepartment
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using Models;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateDepartmentCommandModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        //        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }
    }
}