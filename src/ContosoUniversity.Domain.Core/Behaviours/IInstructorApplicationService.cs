namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService
{
    public interface IInstructorApplicationService
    {
        CreateInstructorWithCourses.Response CreateInstructorWithCourses(CreateInstructorWithCourses.Request request);

        DeleteInstructor.Response DeleteInstructor(DeleteInstructor.Request request);

        ModifyInstructorAndCourses.Response ModifyInstructorAndCourses(ModifyInstructorAndCourses.Request request);
    }
}
