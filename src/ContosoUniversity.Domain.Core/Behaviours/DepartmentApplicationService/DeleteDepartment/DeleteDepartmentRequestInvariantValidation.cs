namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.DeleteDepartment
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class DeleteDepartmentRequestInvariantValidation : UserDomainRequestInvariantValidation<DeleteDepartmentRequest, DeleteDepartmentCommandModel>
    {
        public DeleteDepartmentRequestInvariantValidation(DeleteDepartmentRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
