namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System.ComponentModel.DataAnnotations;

    public class UpdateCourse
    {
        // UpdateCourse.CommandModel
        public class CommandModel
        {
            public int CourseID { get; set; }
            public int DepartmentID { get; set; }

            // [Required] 
            [StringLength(50, MinimumLength = 3)]
            public string Title { get; set; }

            [Range(1, 5)]
            public int Credits { get; set; }

        }

        // UpdateCourse.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // UpdateCourse.Response
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

        // UpdateCourse.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }

            public override void ValidateCommandModel()
            {
                Assert(Context.CommandModel.Title != null, "Title cannot be null");
                Assert(Context.CommandModel.Title != "Title", "Title cannot be set to Title");
            }
        }

        // UpdateCourse.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }

            public override void Validate()
            {
                Validate(Context.CommandModel.CourseID > 0, "CourseId", "CourseId cannot be less than 1");
                Validate(Context.CommandModel.DepartmentID > 0, "DepartmentId", "DepartmentId cannot be less than 1");
            }
        }
    }
}
