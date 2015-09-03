namespace ContosoUniversity.Core.Domain
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public abstract class DomainRequest<TCommandModel> : IDomainAssertable, IDomainValidatable<TCommandModel> where TCommandModel : class
    {
        protected DomainRequest(string userId, TCommandModel commandModel)
        {
            UserId = userId;
            CommandModel = commandModel;
        }

        public string UserId
        {
            get;
        }

        public TCommandModel CommandModel
        {
            get;
        }

        public IInvariantValidation InvariantValidation
        {
            get;
            protected set;
        }

        public IContextualValidation ContextualValidation
        {
            get;
            protected set;
        }

        public void SetInvariantSpecfication(IInvariantValidation specification)
        {
            InvariantValidation = specification;
        }

        public void SetValidationSpecfication(IContextualValidation specification)
        {
            ContextualValidation = specification;
        }

        public void Assert(params object[] dependentServices)
        {
            if (InvariantValidation == null)
                throw new ContosoUniversityException("InvariantSpecification cannot be null");

            InvariantValidation.Assert(dependentServices);
        }

        public ValidationMessageCollection Validate(params object[] dependentServices)
        {
            if (ContextualValidation == null)
                throw new ContosoUniversityException("ValidationSpecification cannot be null");

            return ContextualValidation.Validate(dependentServices);
        }
    }
}
