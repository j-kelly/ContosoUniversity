namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.ModifyStudent
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class ModifyStudentResponse : DomainResponse
    {
        public ModifyStudentResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public ModifyStudentResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
