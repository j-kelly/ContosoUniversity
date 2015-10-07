namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using Behaviours.Courses;
    using Domain.Core.Repository.Containers;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int CourseID { get; internal set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; internal set; }

        [Range(0, 5)]
        public int Credits { get; internal set; }

        public int DepartmentID { get; internal set; }

        public virtual Department Department { get; internal set; }

        public virtual ICollection<Enrollment> Enrollments { get; internal set; }

        public virtual ICollection<Instructor> Instructors { get; internal set; }

        public EntityStateWrapperContainer Modify(CourseUpdate.CommandModel commandModel)
        {
            CourseID = commandModel.CourseID;
            DepartmentID = commandModel.DepartmentID;
            Credits = commandModel.Credits;
            Title = commandModel.Title;

            return new EntityStateWrapperContainer().ModifyEntity(this);
        }

        public EntityStateWrapperContainer Delete()
        {
            return new EntityStateWrapperContainer().DeleteEntity(this);
        }
    }
}