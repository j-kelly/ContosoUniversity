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
    }
}