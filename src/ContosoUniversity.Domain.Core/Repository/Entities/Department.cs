namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService;
    using ContosoUniversity.Domain.Core.Repository.Containers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Department : TrackedEntity
    {
        public int DepartmentID { get; internal set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; internal set; }

        [Column(TypeName = "money")]
        public decimal Budget { get; internal set; }

        public DateTime StartDate { get; internal set; }

        public int? InstructorID { get; internal set; }

        [Timestamp]
        public byte[] RowVersion { get; internal set; }

        public virtual Instructor Administrator { get; private set; }

        public virtual ICollection<Course> Courses { get; private set; }

        public EntityStateWrapperContainer Delete()
        {
            Administrator = null;
            return new EntityStateWrapperContainer().DeleteEntity(this);
        }

        public EntityStateWrapperContainer Modify(UpdateDepartment.CommandModel commandModel)
        {
            DepartmentID = commandModel.DepartmentID;
            Budget = commandModel.Budget;
            InstructorID = commandModel.InstructorID;
            Name = commandModel.Name;
            RowVersion = commandModel.RowVersion;
            StartDate = commandModel.StartDate;

            return new EntityStateWrapperContainer().ModifyEntity(this);
        }

        public EntityStateWrapperContainer SetInstructorId(int? instructorId)
        {
            InstructorID = instructorId;
            return new EntityStateWrapperContainer().ModifyEntity(this);
        }
    }
}