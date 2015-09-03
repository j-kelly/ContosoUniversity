namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.CreateCourse
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateCourseRequestContextualValidation : ContextualValidation<CreateCourseRequest, CreateCourseCommandModel>
    {
        public CreateCourseRequestContextualValidation(CreateCourseRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
