namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.DeleteDepartment
{
    public class DeleteDepartmentCommandModel
    {
        public int DepartmentID { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
