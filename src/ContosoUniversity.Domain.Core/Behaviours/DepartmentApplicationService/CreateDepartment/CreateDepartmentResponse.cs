namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.CreateDepartment
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateDepartmentResponse : DomainResponse
    {
        public CreateDepartmentResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public CreateDepartmentResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
