namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.DeleteInstructor
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteInstructorResponse : DomainResponse
    {
        public DeleteInstructorResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public DeleteInstructorResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
