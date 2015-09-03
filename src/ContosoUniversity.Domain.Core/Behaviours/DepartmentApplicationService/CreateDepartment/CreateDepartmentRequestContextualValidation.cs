namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.CreateDepartment
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
    using NRepository.Core.Query;
    using NRepository.EntityFramework.Query;

    public class CreateDepartmentRequestContextualValidation : ContextualValidation<CreateDepartmentRequest, CreateDepartmentCommandModel>
    {
        public CreateDepartmentRequestContextualValidation(CreateDepartmentRequest context)
            : base(context)
        {
        }

        public override void Validate(ValidationMessageCollection validationMessages)
        {
            ValidateOneAdministratorAssignmentPerInstructor(validationMessages);
        }

        private void ValidateOneAdministratorAssignmentPerInstructor(ValidationMessageCollection validationMessages)
        {
            if (Context.CommandModel.InstructorID == null)
                return;

            var queryRepository = ResolveService<IQueryRepository>();
            var duplicateDepartment = queryRepository.GetEntity<Department>(
                p => p.InstructorID == Context.CommandModel.InstructorID.Value,
                new AsNoTrackingQueryStrategy(),
                new EagerLoadingQueryStrategy<Department>(p => p.Administrator),
                false);

            if (duplicateDepartment != null)
            {
                string errorMessage =
                    $"Instructor {duplicateDepartment.Administrator.FirstMidName} {duplicateDepartment.Administrator.LastName} " +
                    $"is already administrator of the {duplicateDepartment.Name} department.";

                validationMessages.Add(string.Empty, errorMessage);
            }
        }
    }
}
