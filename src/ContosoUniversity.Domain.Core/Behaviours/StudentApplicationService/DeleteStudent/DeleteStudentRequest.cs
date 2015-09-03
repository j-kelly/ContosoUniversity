namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.DeleteStudent
{
    using ContosoUniversity.Core.Domain;

    public class DeleteStudentRequest : DomainRequest<DeleteStudentCommandModel>
    {
        public DeleteStudentRequest(
           string userId, DeleteStudentCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new DeleteStudentRequestInvariantValidation(this);
            ContextualValidation = new DeleteStudentRequestContextualValidation(this);
        }
    }
}
