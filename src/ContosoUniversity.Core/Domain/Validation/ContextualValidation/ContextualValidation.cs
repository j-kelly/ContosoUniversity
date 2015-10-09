namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class ContextualValidation<T, TCommandModel> : IContextualValidation
        where TCommandModel : class
        where T : class, IDomainRequest//IDomainValidatable<TCommandModel>
    {
        private static IEnumerable<object> _DependentServices = null;

        protected ContextualValidation(T context)
        {
            Context = context;
            ValidationMessageCollection = new ValidationMessageCollection();
        }

        public T Context
        {
            get;
        }

        protected ValidationMessageCollection ValidationMessageCollection
        {
            get;
        }

        public virtual void ValidateContext() { /* No Op */ }

        public ValidationMessageCollection Validate(params object[] dependentServices)
        {
            _DependentServices = dependentServices;

            CheckAttributes();
            ValidateContext();

            return ValidationMessageCollection;
        }

        protected TInterface ResolveService<TInterface>()
        {
            return _DependentServices.OfType<TInterface>().Single();
        }

        protected void Validate(bool predicate, string propertyName, string errorMessage)
        {
            if (!predicate)
                ValidationMessageCollection.Add(propertyName, errorMessage);
        }

        protected void Validate(bool predicate, ValidationMessage validationMessage)
        {
            if (!predicate)
                ValidationMessageCollection.Add(validationMessage);
        }

        private void CheckAttributes()
        {
            var properties = Context.CommandModel.GetType().GetProperties();
            foreach (var pi in properties)
            {
                pi.GetCustomAttributes(typeof(ValidationAttribute), true).Select(x => (ValidationAttribute)x).ToList().ForEach(attrb =>
                {
                    var value = pi.GetValue(Context.CommandModel);
                    if (!attrb.IsValid(value))
                    {
                        var msg = attrb.FormatErrorMessage(pi.Name);
                        ValidationMessageCollection.Add(pi.Name, msg);
                    }
                });
            }
        }
    }
}
