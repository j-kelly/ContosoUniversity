namespace ContosoUniversity.Domain.Core.Behaviours.Departments
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class DepartmentDelete
    {
        // DepartmentDelete.CommandModel
        public class CommandModel : ICommandModel
        {
            public int DepartmentID { get; set; }
            public byte[] RowVersion { get; set; }
        }

        // DepartmentDelete.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // DepartmentDelete.Response
        public class Response : DomainResponse
        {
            public Response(ValidationMessageCollection validationDetails)
            : base(validationDetails)
            {
            }

            public Response(ValidationMessageCollection validationDetails, bool hasConcurrencyError)
            : base(validationDetails)
            {
                HasConcurrencyError = hasConcurrencyError;
            }

            public bool? HasConcurrencyError { get; }
        }

        // DepartmentDelete.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // DepartmentDelete.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
