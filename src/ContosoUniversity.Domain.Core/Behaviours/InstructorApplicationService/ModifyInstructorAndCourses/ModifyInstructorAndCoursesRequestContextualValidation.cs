namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.ModifyInstructorAndCourses
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class ModifyInstructorAndCoursesRequestContextualValidation : ContextualValidation<ModifyInstructorAndCoursesRequest, ModifyInstructorAndCoursesCommandModel>
    {
        public ModifyInstructorAndCoursesRequestContextualValidation(ModifyInstructorAndCoursesRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
