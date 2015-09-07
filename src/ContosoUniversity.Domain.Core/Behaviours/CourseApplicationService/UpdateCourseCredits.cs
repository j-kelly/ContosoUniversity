namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class UpdateCourseCredits
    {
        // UpdateCourseCredits.CommandModel
        public class CommandModel
        {
            public int Multiplier { get; set; }
        }

        // UpdateCourseCredits.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // UpdateCourseCredits.Response
        public class Response : DomainResponse
        {
            public Response(int rowsEffected)
            {
                RowsEffected = rowsEffected;
            }

            public Response(ValidationMessageCollection validationDetails)
                : base(validationDetails)
            {
            }

            public int? RowsEffected { get; }
        }

        // UpdateCourseCredits.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // UpdateCourseCredits.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
