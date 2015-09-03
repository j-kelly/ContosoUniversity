namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.CreateStudent
{
    using ContosoUniversity.Core.Domain;

    public class CreateStudentRequest : DomainRequest<CreateStudentCommandModel>
    {
        public CreateStudentRequest(
           string userId, CreateStudentCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new CreateStudentRequestInvariantValidation(this);
            ContextualValidation = new CreateStudentRequestContextualValidation(this);
        }
    }
}
