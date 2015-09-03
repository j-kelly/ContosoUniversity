namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}