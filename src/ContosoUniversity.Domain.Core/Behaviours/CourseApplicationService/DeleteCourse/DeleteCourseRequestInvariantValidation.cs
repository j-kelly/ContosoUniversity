namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.DeleteCourse
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class DeleteCourseRequestInvariantValidation : UserDomainRequestInvariantValidation<DeleteCourseRequest, DeleteCourseCommandModel>
    {
        public DeleteCourseRequestInvariantValidation(DeleteCourseRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
