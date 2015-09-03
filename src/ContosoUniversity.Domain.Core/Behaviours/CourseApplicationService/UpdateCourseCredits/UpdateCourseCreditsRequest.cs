namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourseCredits
{
    using ContosoUniversity.Core.Domain;

    public class UpdateCourseCreditsRequest : DomainRequest<UpdateCourseCreditsCommandModel>
    {
        public UpdateCourseCreditsRequest(
           string userId, UpdateCourseCreditsCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new UpdateCourseCreditsRequestInvariantValidation(this);
            ContextualValidation = new UpdateCourseCreditsRequestContextualValidation(this);
        }
    }
}
