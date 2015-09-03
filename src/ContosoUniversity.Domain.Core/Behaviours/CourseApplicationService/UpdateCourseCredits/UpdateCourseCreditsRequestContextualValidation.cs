namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourseCredits
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class UpdateCourseCreditsRequestContextualValidation : ContextualValidation<UpdateCourseCreditsRequest, UpdateCourseCreditsCommandModel>
    {
        public UpdateCourseCreditsRequestContextualValidation(UpdateCourseCreditsRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
