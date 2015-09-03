namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.CreateStudent
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateStudentResponse : DomainResponse
    {
        public CreateStudentResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public CreateStudentResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
