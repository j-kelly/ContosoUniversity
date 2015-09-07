namespace ContosoUniversity.Domain.Core.Repository.Entities
{
    using ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService;
    using ContosoUniversity.Domain.Core.Repository.Containers;
    using System;
    using System.Collections.Generic;

    public class Student : Person, ISoftDelete
    {
        public DateTime EnrollmentDate { get; internal set; }

        public bool IsDeleted { get; internal set; }

        public virtual ICollection<Enrollment> Enrollments { get; internal set; }

        public EntityStateWrapperContainer Delete()
        {
            return new EntityStateWrapperContainer().DeleteEntity(this);
        }

        public void SetSoftDelete()
        {
            IsDeleted = true;
        }

        public EntityStateWrapperContainer Modify(ModifyStudent.CommandModel commandModel)
        {
            EnrollmentDate = commandModel.EnrollmentDate;
            FirstMidName = commandModel.FirstMidName;
            LastName = commandModel.LastName;

            return new EntityStateWrapperContainer().ModifyEntity(this);
        }
    }
}