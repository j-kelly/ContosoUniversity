namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.DeleteCourse
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteCourseRequestContextualValidation : ContextualValidation<DeleteCourseRequest, DeleteCourseCommandModel>
    {
        public DeleteCourseRequestContextualValidation(DeleteCourseRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
