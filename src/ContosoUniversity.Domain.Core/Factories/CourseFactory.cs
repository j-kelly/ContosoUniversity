namespace ContosoUniversity.Domain.Core.Factories
{
    using ContosoUniversity.Domain.Core.Behaviours.Courses;
    using ContosoUniversity.Domain.Core.Repository.Entities;

    public static class CourseFactory
    {
        public static Course CreatePartial(int courseId)
        {
            return new Course { CourseID = courseId };
        }

        public static Course Create(CourseCreate.CommandModel commandModel)
        {
            var course = new Course
            {
                CourseID = commandModel.CourseID,
                DepartmentID = commandModel.DepartmentID,
                Title = commandModel.Title,
                Credits = commandModel.Credits
            };

            return course;
        }
    }
}