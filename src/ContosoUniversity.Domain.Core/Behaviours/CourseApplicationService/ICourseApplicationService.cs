namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using CreateCourse;
    using DeleteCourse;
    using UpdateCourse;
    using UpdateCourseCredits;

    public interface ICourseApplicationService
    {
        UpdateCourseResponse UpdateCourse(UpdateCourseRequest request);

        CreateCourseResponse CreateCourse(CreateCourseRequest request);

        DeleteCourseResponse DeleteCourse(DeleteCourseRequest request);

        UpdateCourseCreditsResponse UpdateCourseCredits(UpdateCourseCreditsRequest request);
    }
}
