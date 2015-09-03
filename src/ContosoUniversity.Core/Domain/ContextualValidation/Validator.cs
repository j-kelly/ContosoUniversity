namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    using System;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public static class Validator
    {
        public static ValidationMessageCollection ValidateRequest<TCommandModel>(IDomainValidatable<TCommandModel> model, params object[] dependentServices) where TCommandModel : class
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "model is null.");

            if (model is IDomainAssertable)
                ((IDomainAssertable)model).Assert(dependentServices);

            return model.Validate(dependentServices);
        }

        public static bool IsSpecificationValid<TCommandModel>(IDomainValidatable<TCommandModel> model, params object[] dependentServices) where TCommandModel : class
        {
            return model.Validate(dependentServices).HasValidationIssues;
        }
    }
}
