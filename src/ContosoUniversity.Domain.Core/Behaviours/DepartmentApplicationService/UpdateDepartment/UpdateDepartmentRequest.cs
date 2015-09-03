namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.UpdateDepartment
{
    using ContosoUniversity.Core.Domain;

    public class UpdateDepartmentRequest : DomainRequest<UpdateDepartmentCommandModel>
    {
        public UpdateDepartmentRequest(
           string userId, UpdateDepartmentCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new UpdateDepartmentRequestInvariantValidation(this);
            ContextualValidation = new UpdateDepartmentRequestContextualValidation(this);
        }
    }
}
