namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService
{

    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateStudent
    {
        // CreateStudent.CommandModel
        public class CommandModel
        {
            [Required]
            [StringLength(50)]
            public string LastName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
            public string FirstMidName { get; set; }

            public DateTime EnrollmentDate { get; set; } = DateTimeHelper.Today;
        }

        // CreateStudent.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // CreateStudent.Response
        public class Response : DomainResponse
        {
            public Response()
            {
            }

            public Response(ValidationMessageCollection validationDetails)
                : base(validationDetails)
            {
            }
        }

        // CreateStudent.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // CreateStudent.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }

}
