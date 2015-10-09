namespace ContosoUniversity.Domain.Core.Behaviours.Instructors
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InstructorModifyAndCourses
    {
        // InstructorModifyAndCourses.CommandModel
        public class CommandModel : ICommandModel
        {
            public int InstructorId { get; set; }

            [Required]
            [StringLength(50)]
            public string LastName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
            public string FirstMidName { get; set; }

            public DateTime HireDate { get; set; }

            [StringLength(50)]
            public string OfficeLocation { get; set; }

            public IEnumerable<int> SelectedCourses { get; set; }
        }

        // InstructorModifyAndCourses.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // InstructorModifyAndCourses.Response
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

        // InstructorModifyAndCourses.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // InstructorModifyAndCourses.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }
        }
    }
}
