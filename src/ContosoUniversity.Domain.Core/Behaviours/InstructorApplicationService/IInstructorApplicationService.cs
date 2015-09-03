namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService
{
    using CreateInstructorWithCourses;
    using DeleteInstructor;
    using ModifyInstructorAndCourses;

    public interface IInstructorApplicationService
    {
        CreateInstructorWithCoursesResponse CreateInstructorWithCourses(CreateInstructorWithCoursesRequest request);

        DeleteInstructorResponse DeleteInstructor(DeleteInstructorRequest request);

        ModifyInstructorAndCoursesResponse ModifyInstructorAndCourses(ModifyInstructorAndCoursesRequest request);
    }
}
