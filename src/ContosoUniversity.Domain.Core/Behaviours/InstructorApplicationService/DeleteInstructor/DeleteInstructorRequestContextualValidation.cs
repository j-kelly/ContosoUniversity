namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.DeleteInstructor
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteInstructorRequestContextualValidation : ContextualValidation<DeleteInstructorRequest, DeleteInstructorCommandModel>
    {
        public DeleteInstructorRequestContextualValidation(DeleteInstructorRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
