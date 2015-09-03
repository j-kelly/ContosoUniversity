namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.CreateStudent
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class CreateStudentRequestInvariantValidation : UserDomainRequestInvariantValidation<CreateStudentRequest, CreateStudentCommandModel>
    {
        public CreateStudentRequestInvariantValidation(CreateStudentRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
