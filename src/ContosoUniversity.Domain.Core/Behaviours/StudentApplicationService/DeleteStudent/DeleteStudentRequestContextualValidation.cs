namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.DeleteStudent
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteStudentRequestContextualValidation : ContextualValidation<DeleteStudentRequest, DeleteStudentCommandModel>
    {
        public DeleteStudentRequestContextualValidation(DeleteStudentRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
