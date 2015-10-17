namespace ContosoUniversity.Domain.Core.Behaviours.Instructors
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class InstructorDelete
    {
        // InstructorDelete.CommandModel
        public class CommandModel : ICommandModel
        {
            public int InstructorId { get; set; }
        }

        // InstructorDelete.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // InstructorDelete.Response
        public class Response : DomainResponse
        {
            public Response() { }

            // If you are using the auto-validation 'decorator' do not change the signiture of this ctor (see AutoValidate<T>) in the 
            // DomainBootstrapper class
            public Response(ValidationMessageCollection validationDetails)
                : base(validationDetails)
            {
            }
        }

        // InstructorDelete.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // InstructorDelete.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
