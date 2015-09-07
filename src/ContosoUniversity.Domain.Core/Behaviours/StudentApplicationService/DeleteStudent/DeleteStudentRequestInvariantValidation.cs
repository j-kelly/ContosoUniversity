namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService.DeleteStudent
{
    using ContosoUniversity.Core.Domain.InvariantValidation;

    public class DeleteStudentRequestInvariantValidation : UserDomainRequestInvariantValidation<DeleteStudentRequest, DeleteStudentCommandModel>
    {
        public DeleteStudentRequestInvariantValidation(DeleteStudentRequest context)
            : base(context)
        {
        }


    }
}
