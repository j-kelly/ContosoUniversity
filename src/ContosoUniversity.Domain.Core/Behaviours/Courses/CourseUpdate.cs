namespace ContosoUniversity.Domain.Core.Behaviours.Courses
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System.ComponentModel.DataAnnotations;

    public class CourseUpdate
    {
        // CourseUpdate.CommandModel
        public class CommandModel : ICommandModel
        {
            public int CourseID { get; set; }
            public int DepartmentID { get; set; }

            // [Required] 
            [StringLength(50, MinimumLength = 3)]
            public string Title { get; set; }

            [Range(1, 5)]
            public int Credits { get; set; }

        }

        // CourseUpdate.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // CourseUpdate.Response
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

        // CourseUpdate.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }

            public override void ValidateContext()
            {
                base.ValidateContext();

                Assert(Context.CommandModel.Title != null, "Title cannot be null");
                Assert(Context.CommandModel.Title != "Title", "Title cannot be set to Title");
            }
        }

        // CourseUpdate.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }

            public override void ValidateContext()
            {
                Validate(Context.CommandModel.CourseID > 0, "CourseId", "CourseId cannot be less than 1");
                Validate(Context.CommandModel.DepartmentID > 0, "DepartmentId", "DepartmentId cannot be less than 1");
            }
        }
    }
}
