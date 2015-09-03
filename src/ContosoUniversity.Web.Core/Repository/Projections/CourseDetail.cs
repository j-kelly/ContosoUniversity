namespace ContosoUniversity.Web.Core.Repository.Projections
{
    using System.ComponentModel.DataAnnotations;

    public class CourseDetail
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }

        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}