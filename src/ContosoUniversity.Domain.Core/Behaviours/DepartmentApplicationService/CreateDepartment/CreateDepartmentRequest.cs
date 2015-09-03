namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.CreateDepartment
{
    using ContosoUniversity.Core.Domain;

    public class CreateDepartmentRequest : DomainRequest<CreateDepartmentCommandModel>
    {
        public CreateDepartmentRequest(
           string userId, CreateDepartmentCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new CreateDepartmentRequestInvariantValidation(this);
            ContextualValidation = new CreateDepartmentRequestContextualValidation(this);
        }
    }
}
