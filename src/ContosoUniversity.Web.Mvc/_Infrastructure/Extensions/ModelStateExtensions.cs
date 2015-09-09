namespace System.Web.Mvc
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using System.Collections.Generic;

    public static class ModelStateExtensions
    {
        public static void AddRange(this ModelStateDictionary modelState, IEnumerable<ValidationMessage> messages)
        {
            modelState.Clear();
            foreach (var msg in messages)
            {
                modelState.AddModelError(msg.PropertyName, msg.ErrorMessage);
            }
        }

        public static void AddRange(this ModelStateDictionary modelState, ValidationMessageCollection messageCollection)
        {
            modelState.Clear();
            foreach (var msg in messageCollection.AllValidationMessages)
            {
                modelState.AddModelError(msg.PropertyName, msg.ErrorMessage);
            }
        }
    }
}