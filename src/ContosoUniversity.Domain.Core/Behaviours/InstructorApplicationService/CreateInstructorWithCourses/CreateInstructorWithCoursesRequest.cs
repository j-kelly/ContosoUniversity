namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses
{
    using ContosoUniversity.Core.Domain;

    public class CreateInstructorWithCoursesRequest : DomainRequest<CreateInstructorWithCoursesCommandModel>
    {
        public CreateInstructorWithCoursesRequest(
           string userId, CreateInstructorWithCoursesCommandModel commandModel)
            : base(userId, commandModel)
        {
            InvariantValidation = new CreateInstructorWithCoursesRequestInvariantValidation(this);
            ContextualValidation = new CreateInstructorWithCoursesRequestContextualValidation(this);
        }
    }
}
