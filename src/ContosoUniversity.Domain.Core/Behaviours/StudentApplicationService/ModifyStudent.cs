namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ModifyStudent
    {
        // ModifyStudent.CommandModel
        public class CommandModel
        {
            public int ID { get; set; }

            [Required]
            [StringLength(50)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
            [Display(Name = "First Name")]
            public string FirstMidName { get; set; }

            [Display(Name = "Full Name")]
            public string FullName => $"{LastName}, {FirstMidName}";

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [Display(Name = "Enrollment Date")]
            public DateTime EnrollmentDate { get; set; }
        }

        // ModifyStudent.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // ModifyStudent.Response
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

        // ModifyStudent.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // ModifyStudent.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
