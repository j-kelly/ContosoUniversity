namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.ModifyInstructorAndCourses
{
    using ContosoUniversity.Core.Domain;

    public class ModifyInstructorAndCoursesRequest : DomainRequest<ModifyInstructorAndCoursesCommandModel>
    {
        public ModifyInstructorAndCoursesRequest(
           string userId, ModifyInstructorAndCoursesCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new ModifyInstructorAndCoursesRequestInvariantValidation(this);
            ContextualValidation = new ModifyInstructorAndCoursesRequestContextualValidation(this);
        }
    }
}
