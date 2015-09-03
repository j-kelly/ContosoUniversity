namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.DeleteDepartment
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteDepartmentRequestContextualValidation : ContextualValidation<DeleteDepartmentRequest, DeleteDepartmentCommandModel>
    {
        public DeleteDepartmentRequestContextualValidation(DeleteDepartmentRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
