namespace ContosoUniversity.Domain.Core.Behaviours.Courses
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System.ComponentModel.DataAnnotations;

    public class CourseDelete
    {
        // CourseDelete.CommandModel
        public class CommandModel : ICommandModel
        {
            [Range(1, int.MaxValue, ErrorMessage = "CourseId must have a minimum value of 1")]
            public int CourseId { get; set; }
        }

        // CourseDelete.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // CourseDelete.Response
        public class Response : DomainResponse
        {
            public Response(ValidationMessageCollection validationDetails = null)
               : base(validationDetails)
            {
            }
        }

        // CourseDelete.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // CourseDelete.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
