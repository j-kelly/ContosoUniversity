namespace ContosoUniversity.Core.Domain.ContextualValidation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract class ContextualValidation<T, TCommandModel> : IContextualValidation
        where TCommandModel : class
        where T : class, IDomainValidatable<TCommandModel>
    {
        private static IEnumerable<object> _DependentServices = null;

        protected ContextualValidation(T context)
        {
            Context = context;
        }

        public T Context
        {
            get;
        }

        public abstract void Validate(ValidationMessageCollection validationMessages);

        public ValidationMessageCollection Validate(params object[] dependentServices)
        {
            _DependentServices = dependentServices;

            var messages = new ValidationMessageCollection();
            CheckAttributes(messages);
            Validate(messages);

            return messages;
        }

        protected TInterface ResolveService<TInterface>()
        {
            return _DependentServices.OfType<TInterface>().Single();
        }

        private void CheckAttributes(ValidationMessageCollection messages)
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
                        messages.Add(pi.Name, msg);
                    }
                });
            }
        }
    }
}
