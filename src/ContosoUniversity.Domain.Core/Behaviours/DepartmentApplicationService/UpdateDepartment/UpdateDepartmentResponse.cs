namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.UpdateDepartment
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class UpdateDepartmentResponse : DomainResponse
    {
        public UpdateDepartmentResponse(ValidationMessageCollection validationDetails, byte[] rowVersion = null)
            : base(validationDetails)
        {
            RowVersion = rowVersion;
        }

        public byte[] RowVersion
        {
            get;
        }
    }
}
