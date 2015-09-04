using ContosoUniversity.Domain.Core.Repository;
using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student : Person, ISoftDelete
    {
        public DateTime EnrollmentDate { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }

    }
}