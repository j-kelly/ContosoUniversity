namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService
{
    using CreateStudent;
    using DeleteStudent;
    using ModifyStudent;

    public interface IStudentApplicationService
    {
        CreateStudentResponse CreateStudent(CreateStudentRequest request);

        DeleteStudentResponse DeleteStudent(DeleteStudentRequest request);

        ModifyStudentResponse ModifyStudent(ModifyStudentRequest request);
    }
}
