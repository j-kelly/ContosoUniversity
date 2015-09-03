namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DepartmentDetail
    {
        public int DepartmentID { get; set; }

        public string Administrator { get; set; }

        public string Name { get; set; }

        public decimal Budget { get; set; }

        public byte[] RowVersion { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
    }
}
