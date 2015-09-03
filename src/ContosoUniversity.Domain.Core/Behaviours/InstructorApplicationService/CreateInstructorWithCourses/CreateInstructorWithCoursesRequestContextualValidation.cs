namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateInstructorWithCoursesRequestContextualValidation : ContextualValidation<CreateInstructorWithCoursesRequest, CreateInstructorWithCoursesCommandModel>
    {
        public CreateInstructorWithCoursesRequestContextualValidation(CreateInstructorWithCoursesRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
