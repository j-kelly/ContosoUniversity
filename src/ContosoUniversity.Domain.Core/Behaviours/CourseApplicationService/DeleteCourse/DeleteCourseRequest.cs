namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.DeleteCourse
{
    using ContosoUniversity.Core.Domain;

    public class DeleteCourseRequest : DomainRequest<DeleteCourseCommandModel>
    {
        public DeleteCourseRequest(
           string userId, DeleteCourseCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new DeleteCourseRequestInvariantValidation(this);
            ContextualValidation = new DeleteCourseRequestContextualValidation(this);
        }
    }
}
