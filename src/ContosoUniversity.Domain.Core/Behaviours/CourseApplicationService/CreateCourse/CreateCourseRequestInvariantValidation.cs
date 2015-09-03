namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.CreateCourse
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class CreateCourseRequestInvariantValidation : UserDomainRequestInvariantValidation<CreateCourseRequest, CreateCourseCommandModel>
    {
        public CreateCourseRequestInvariantValidation(CreateCourseRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
