namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.ModifyStudent
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class ModifyStudentRequestInvariantValidation : UserDomainRequestInvariantValidation<ModifyStudentRequest, ModifyStudentCommandModel>
    {
        public ModifyStudentRequestInvariantValidation(ModifyStudentRequest context)
            : base(context)
        {
        }

        public void CommandModelCannotBeNull()
        {
            Assert(Context.CommandModel != null);
        }
    }
}
