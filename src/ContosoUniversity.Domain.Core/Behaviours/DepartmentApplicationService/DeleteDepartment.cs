namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class DeleteDepartment
    {
        // DeleteDepartment.CommandModel
        public class CommandModel
        {
            public int DepartmentID { get; set; }
            public byte[] RowVersion { get; set; }
        }

        // DeleteDepartment.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // DeleteDepartment.Response
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

        // DeleteDepartment.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // DeleteDepartment.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
