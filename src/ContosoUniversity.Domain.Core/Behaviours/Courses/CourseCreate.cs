namespace ContosoUniversity.Domain.Core.Behaviours.Courses
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System.ComponentModel.DataAnnotations;

    public class CourseCreate
    {
        // CourseCreate.CommandModel
        public class CommandModel : ICommandModel
        {
            public int CourseID { get; set; }

            [StringLength(50, MinimumLength = 3)]
            public string Title { get; set; }

            [Range(1, 5)]
            public int Credits { get; set; }

            public int DepartmentID { get; set; }
        }

        // CourseCreate.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // CourseCreate.Response
        public class Response : DomainResponse
        {
            // If you are using the auto-validation 'decorator' do not change the signiture of this ctor (see AutoValidate<T>) in the 
            // DomainBootstrapper class
            public Response(ValidationMessageCollection validationDetails)
                : base(validationDetails)
            {
            }

            public Response(ValidationMessageCollection validationDetails, int? courseId)
                : base(validationDetails)
            {
                CourseId = courseId;
            }

            public int? CourseId { get; }
        }

        // CourseCreate.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // CourseCreate.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }

            public override void ValidateContext()
            {
                var deptId = Context.CommandModel.DepartmentID;
                Validate(deptId > 0, "DepartmentID", "Missing Department");
            }
        }
    }
}
