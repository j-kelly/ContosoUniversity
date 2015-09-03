namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.CreateDepartment
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class CreateDepartmentRequestInvariantValidation : UserDomainRequestInvariantValidation<CreateDepartmentRequest, CreateDepartmentCommandModel>
    {
        public CreateDepartmentRequestInvariantValidation(CreateDepartmentRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
