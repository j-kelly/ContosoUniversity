namespace ContosoUniversity.Domain.Core.Behaviours.DepartmentApplicationService.UpdateDepartment
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class UpdateDepartmentRequestInvariantValidation : UserDomainRequestInvariantValidation<UpdateDepartmentRequest, UpdateDepartmentCommandModel>
    {
        public UpdateDepartmentRequestInvariantValidation(UpdateDepartmentRequest context)
            : base(context)
        {
        }

    }
}
