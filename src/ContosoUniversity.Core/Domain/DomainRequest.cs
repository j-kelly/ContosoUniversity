namespace ContosoUniversity.Core.Domain
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System;
    using Utilities;

    public abstract class DomainRequest<TCommandModel> : IDomainRequest where TCommandModel : class, ICommandModel
    {
        protected DomainRequest(string userId, TCommandModel commandModel)
        {
            Check.NotNullOrWhiteSpace(userId, nameof(userId));
            Check.NotNull(userId, nameof(commandModel));

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
            internal set;
        }

        ICommandModel IDomainRequest.CommandModel
        {
            get { return CommandModel; }
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

        public ValidationMessageCollection Assert(params object[] dependentServices)
        {
            if (InvariantValidation == null)
                throw new ContosoUniversityException("InvariantSpecification cannot be null");

            if (ContextualValidation == null)
                throw new ContosoUniversityException("ValidationSpecification cannot be null");

            // Check invariant stuff first 
            InvariantValidation.Validate(dependentServices);
            return ContextualValidation.Validate(dependentServices);
        }
    }
}
