namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService
{
    using CreateDepartment;
    using DeleteDepartment;
    using UpdateDepartment;

    public interface IDepartmentApplicationService
    {
        UpdateDepartmentResponse UpdateDepartment(UpdateDepartmentRequest request);

        CreateDepartmentResponse CreateDepartment(CreateDepartmentRequest request);

        DeleteDepartmentResponse DeleteDepartment(DeleteDepartmentRequest request);
    }
}
