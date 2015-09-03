namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateInstructorWithCoursesResponse : DomainResponse
    {
        public CreateInstructorWithCoursesResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public CreateInstructorWithCoursesResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
