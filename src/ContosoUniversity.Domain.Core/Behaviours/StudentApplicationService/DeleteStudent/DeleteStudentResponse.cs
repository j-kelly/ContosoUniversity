namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.DeleteStudent
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteStudentResponse : DomainResponse
    {
        public DeleteStudentResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public DeleteStudentResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
