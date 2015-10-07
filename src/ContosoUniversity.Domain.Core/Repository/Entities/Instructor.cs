namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using Containers;
    using NRepository.Core.Query;
    using Strategies;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Instructor : Person
    {
        public Instructor()
        {
            Courses = new List<Course>();
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; internal set; }

        public virtual List<Course> Courses { get; internal set; }

        public virtual OfficeAssignment OfficeAssignment { get; internal set; }

        public EntityStateWrapperContainer Delete()
        {
            OfficeAssignment = null;
            return new EntityStateWrapperContainer().DeleteEntity(this);
        }

        public EntityStateWrapperContainer Modify(IQueryRepository queryRepository, ContosoUniversity.Domain.Core.Behaviours.Instructors.InstructorModifyAndCourses.CommandModel commandModel)
        {
            var retVal = new EntityStateWrapperContainer();

            // Removals first
            Courses.Clear();
            if (OfficeAssignment != null && commandModel.OfficeLocation == null)
                retVal.DeleteEntity(OfficeAssignment);

            // Update properties
            FirstMidName = commandModel.FirstMidName;
            LastName = commandModel.LastName;
            HireDate = commandModel.HireDate;
            OfficeAssignment = new OfficeAssignment { Location = commandModel.OfficeLocation };

            if (commandModel.SelectedCourses != null)
            {
                Courses = queryRepository.GetEntities<Course>(
                    new FindByIdsSpecificationStrategy<Course>(p => p.CourseID, commandModel.SelectedCourses)).ToList();
            }

            retVal.ModifyEntity(this);
            return retVal;
        }
    }
}