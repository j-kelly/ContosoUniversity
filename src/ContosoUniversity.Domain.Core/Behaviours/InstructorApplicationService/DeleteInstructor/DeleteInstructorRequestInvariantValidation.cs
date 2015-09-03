namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.DeleteInstructor
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class DeleteInstructorRequestInvariantValidation : UserDomainRequestInvariantValidation<DeleteInstructorRequest, DeleteInstructorCommandModel>
    {
        public DeleteInstructorRequestInvariantValidation(DeleteInstructorRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
