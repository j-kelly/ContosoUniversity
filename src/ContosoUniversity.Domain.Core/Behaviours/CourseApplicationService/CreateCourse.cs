namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Core.Domain.InvariantValidation;
    using System.ComponentModel.DataAnnotations;
    public class CreateCourse
    {
        // CreateCourse.CommandModel
        public class CommandModel
        {
            public int CourseID { get; set; }

            [StringLength(50, MinimumLength = 3)]
            public string Title { get; set; }

            [Range(1, 5)]
            public int Credits { get; set; }

            public int DepartmentID { get; set; }
        }

        // CreateCourse.Request
        public class Request : DomainRequest<CommandModel>
        {
            public Request(string userId, CommandModel commandModel)
                : base(userId, commandModel)
            {
                InvariantValidation = new InvariantValidation(this);
                ContextualValidation = new ContextualValidation(this);
            }
        }

        // CreateCourse.Response
        public class Response : DomainResponse
        {
            public Response(ValidationMessageCollection validationDetails, int? courseId = null)
                : base(validationDetails)
            {
                CourseId = courseId;
            }

            public int? CourseId { get; }
        }

        // CreateCourse.InvariantValidation
        public class InvariantValidation : UserDomainRequestInvariantValidation<Request, CommandModel>
        {
            public InvariantValidation(Request context)
                : base(context)
            {
            }
        }

        // CreateCourse.ContextualValidation
        public class ContextualValidation : ContextualValidation<Request, CommandModel>
        {
            public ContextualValidation(Request context)
                : base(context)
            {
            }

            public override void Validate()
            {
                var deptId = Context.CommandModel.DepartmentID;
                Validate(deptId > 0, "DepartmentID", "Missing Department");
            }
        }
    }
}
