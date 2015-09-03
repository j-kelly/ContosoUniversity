namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.CreateCourse
{
    using ContosoUniversity.Core.Domain;

    public class CreateCourseRequest : DomainRequest<CreateCourseCommandModel>
    {
        public CreateCourseRequest(
           string userId, CreateCourseCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new CreateCourseRequestInvariantValidation(this);
            ContextualValidation = new CreateCourseRequestContextualValidation(this);
        }
    }
}
