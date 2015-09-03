namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateCourseCommandModel
    {
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(1, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }
    }
}
