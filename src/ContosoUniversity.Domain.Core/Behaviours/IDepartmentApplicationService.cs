namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService
{
    public interface IDepartmentApplicationService
    {
        UpdateDepartment.Response UpdateDepartment(UpdateDepartment.Request request);

        CreateDepartment.Response CreateDepartment(CreateDepartment.Request request);

        DeleteDepartment.Response DeleteDepartment(DeleteDepartment.Request request);
    }
}
