namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.ModifyStudent
{
    using ContosoUniversity.Core.Domain;

    public class ModifyStudentRequest : DomainRequest<ModifyStudentCommandModel>
    {
        public ModifyStudentRequest(
           string userId, ModifyStudentCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new ModifyStudentRequestInvariantValidation(this);
            ContextualValidation = new ModifyStudentRequestContextualValidation(this);
        }
    }
}
