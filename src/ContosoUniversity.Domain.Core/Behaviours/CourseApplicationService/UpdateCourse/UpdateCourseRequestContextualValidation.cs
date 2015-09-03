namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class UpdateCourseRequestContextualValidation : ContextualValidation<UpdateCourseRequest, UpdateCourseCommandModel>
    {
        public UpdateCourseRequestContextualValidation(UpdateCourseRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
        }
    }
}