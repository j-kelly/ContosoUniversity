namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class CreateInstructorWithCoursesRequestInvariantValidation : UserDomainRequestInvariantValidation<CreateInstructorWithCoursesRequest, CreateInstructorWithCoursesCommandModel>
    {
        public CreateInstructorWithCoursesRequestInvariantValidation(CreateInstructorWithCoursesRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
