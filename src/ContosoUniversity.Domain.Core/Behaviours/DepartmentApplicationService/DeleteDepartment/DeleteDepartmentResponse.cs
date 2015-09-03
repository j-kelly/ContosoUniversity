namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.DeleteDepartment
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteDepartmentResponse : DomainResponse
    {
        public DeleteDepartmentResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }

        public DeleteDepartmentResponse(ValidationMessageCollection validationDetails, bool hasConcurrencyError)
            : base(validationDetails)
        {
            HasConcurrencyError = hasConcurrencyError;
        }

        public bool? HasConcurrencyError { get; }
    }
}
