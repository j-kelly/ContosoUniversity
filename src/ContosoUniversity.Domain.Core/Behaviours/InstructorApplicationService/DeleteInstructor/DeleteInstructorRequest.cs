namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.DeleteInstructor
{
    using ContosoUniversity.Core.Domain;

    public class DeleteInstructorRequest : DomainRequest<DeleteInstructorCommandModel>
    {
        public DeleteInstructorRequest(
           string userId, DeleteInstructorCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new DeleteInstructorRequestInvariantValidation(this);
            ContextualValidation = new DeleteInstructorRequestContextualValidation(this);
        }
    }
}
