namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.ModifyInstructorAndCourses
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class ModifyInstructorAndCoursesRequestInvariantValidation : UserDomainRequestInvariantValidation<ModifyInstructorAndCoursesRequest, ModifyInstructorAndCoursesCommandModel>
    {
        public ModifyInstructorAndCoursesRequestInvariantValidation(ModifyInstructorAndCoursesRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
