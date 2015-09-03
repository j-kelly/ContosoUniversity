using System;
using System.Collections.Generic;

namespace ContosoUniversity.Models
{
    public class Student : Person
    {
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}