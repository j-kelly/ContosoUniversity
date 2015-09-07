namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService
{
    public interface IStudentApplicationService
    {
        CreateStudent.Response CreateStudent(CreateStudent.Request request);

        DeleteStudent.Response DeleteStudent(DeleteStudent.Request request);

        ModifyStudent.Response ModifyStudent(ModifyStudent.Request request);
    }
}
