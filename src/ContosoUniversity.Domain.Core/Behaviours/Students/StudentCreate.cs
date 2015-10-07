namespace ContosoUniversity.Domain.Core.Behaviours.Students
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class StudentCreate
    {
        // StudentCreate.CommandModel
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

        // StudentCreate.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // StudentCreate.Response
        public class Response : DomainResponse
        {
            public Response(ValidationMessageCollection validationDetails, int? studentId = null)
                : base(validationDetails)
            {
                StudentId = studentId;
            }

            public int? StudentId { get; set; }
        }

        // StudentCreate.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // StudentCreate.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
