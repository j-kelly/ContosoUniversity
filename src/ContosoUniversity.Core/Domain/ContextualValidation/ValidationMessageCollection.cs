namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationMessageCollection : List<ValidationMessage>
    {
        public ValidationMessageCollection(params ValidationMessage[] validationMesssages)
        {
            AddRange(validationMesssages);
        }

        public ValidationMessageCollection(IEnumerable<ValidationMessage> validationMesssages)
        {
            AddRange(validationMesssages);
        }

        public IEnumerable<ValidationMessage> AllValidationMessages
        {
            get { return this; }
        }

        public bool HasValidationIssues
        {
            get { return this.Any(); }
        }

        public bool HasErrors
        {
            get { return this.Any(p => p.ValidationLevel == ValidationLevelType.Error); }
        }

        public IEnumerable<ValidationMessage> Errors
        {
            get { return this.Where(p => p.ValidationLevel == ValidationLevelType.Error); }
        }

        public bool HasWarnings
        {
            get { return this.Any(p => p.ValidationLevel == ValidationLevelType.Warning); }
        }

        public IEnumerable<ValidationMessage> Warnings
        {
            get { return this.Where(p => p.ValidationLevel == ValidationLevelType.Warning); }
        }

        public void Add(string propertyName, string errorMessage, ValidationLevelType validationLevel = ValidationLevelType.Error)
        {
            Add(new ValidationMessage(propertyName, errorMessage, validationLevel));
        }
    }
}
