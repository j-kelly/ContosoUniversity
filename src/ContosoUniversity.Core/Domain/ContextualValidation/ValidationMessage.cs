namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    public class ValidationMessage
    {
        public ValidationMessage(string propertyName, string errorMsg, ValidationLevelType validationLevel = ValidationLevelType.Error)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMsg;
            ValidationLevel = validationLevel;
        }

        public ValidationLevelType ValidationLevel
        {
            get;

        }

        public string PropertyName
        {
            get;

        }

        public string ErrorMessage
        {
            get;

        }
    }

}