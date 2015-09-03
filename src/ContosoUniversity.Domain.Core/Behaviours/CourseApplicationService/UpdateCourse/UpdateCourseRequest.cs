namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse
{
    using ContosoUniversity.Core.Domain;

    public class UpdateCourseRequest : DomainRequest<UpdateCourseCommandModel>
    {
        public UpdateCourseRequest(
           string userId, UpdateCourseCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse.UpdateCourseRequestInvariantValidation(this);
            ContextualValidation = new ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse.UpdateCourseRequestContextualValidation(this);
        }
    }
}
