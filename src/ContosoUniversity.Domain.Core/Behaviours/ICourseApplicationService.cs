namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    public interface ICourseApplicationService
    {
        UpdateCourse.Response UpdateCourse(UpdateCourse.Request request);

        CreateCourse.Response CreateCourse(CreateCourse.Request request);

        DeleteCourse.Response DeleteCourse(DeleteCourse.Request request);

        UpdateCourseCredits.Response UpdateCourseCredits(UpdateCourseCredits.Request request);
    }
}
