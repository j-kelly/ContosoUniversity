namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.ModifyStudent
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class ModifyStudentRequestContextualValidation : ContextualValidation<ModifyStudentRequest, ModifyStudentCommandModel>
    {
        public ModifyStudentRequestContextualValidation(ModifyStudentRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
