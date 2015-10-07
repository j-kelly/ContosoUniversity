namespace ContosoUniversity.Domain.Core.Factories
{
    using ContosoUniversity.Domain.Core.Repository.Entities;
    using Repository.Containers;

    public static class CourseFactory
    {
        public static Course CreatePartial(int courseId)
        {
            return new Course { CourseID = courseId };
        }

        public static EntityStateWrapperContainer Create(ContosoUniversity.Domain.Core.Behaviours.Courses.CourseCreate.CommandModel commandModel)
        {
            var course = new Course
            {
                CourseID = commandModel.CourseID,
                DepartmentID = commandModel.DepartmentID,
                Title = commandModel.Title,
                Credits = commandModel.Credits
            };

            return new EntityStateWrapperContainer().AddEntity(course);
        }
    }
}