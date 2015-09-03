namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.DeleteDepartment
{
    using ContosoUniversity.Core.Domain;

    public class DeleteDepartmentRequest : DomainRequest<DeleteDepartmentCommandModel>
    {
        public DeleteDepartmentRequest(
           string userId, DeleteDepartmentCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new DeleteDepartmentRequestInvariantValidation(this);
            ContextualValidation = new DeleteDepartmentRequestContextualValidation(this);
        }
    }
}
