namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    using System;
    using Validation;

    public static class Validator
    {
        public static ValidationMessageCollection ValidateRequest(IAssertable assertable, params object[] dependentServices)
        {
            if (assertable == null)
                throw new ArgumentNullException(nameof(assertable), "request is null.");

            return assertable.Assert(dependentServices);
        }
    }
}