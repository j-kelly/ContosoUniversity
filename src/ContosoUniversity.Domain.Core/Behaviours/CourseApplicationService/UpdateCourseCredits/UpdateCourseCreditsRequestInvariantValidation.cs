namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourseCredits
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class UpdateCourseCreditsRequestInvariantValidation : UserDomainRequestInvariantValidation<UpdateCourseCreditsRequest, UpdateCourseCreditsCommandModel>
    {
        public UpdateCourseCreditsRequestInvariantValidation(UpdateCourseCreditsRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
