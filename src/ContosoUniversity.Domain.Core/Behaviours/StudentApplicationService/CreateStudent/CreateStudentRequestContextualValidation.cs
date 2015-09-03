namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.CreateStudent
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateStudentRequestContextualValidation : ContextualValidation<CreateStudentRequest, CreateStudentCommandModel>
    {
        public CreateStudentRequestContextualValidation(CreateStudentRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            // var queryRepository = ResolveService<IQueryRepository>();
        }
    }
}
