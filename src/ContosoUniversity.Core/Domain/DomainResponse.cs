namespace ContosoUniversity.Core.Domain
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public abstract class DomainResponse : IDomainResponse
    {
        protected DomainResponse(ValidationMessageCollection validationDetails)
        {
            if (validationDetails == null)
                validationDetails = new ValidationMessageCollection();

            ValidationDetails = validationDetails;
        }

        protected DomainResponse()
            : this(new ValidationMessageCollection())
        {
        }

        public ValidationMessageCollection ValidationDetails
        {
            get;
        }

        public bool HasValidationIssues
        {
            get { return ValidationDetails.HasValidationIssues; }
        }
    }
}
