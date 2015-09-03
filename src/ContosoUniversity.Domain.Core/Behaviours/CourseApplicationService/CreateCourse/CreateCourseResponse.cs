namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.CreateCourse
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class CreateCourseResponse : DomainResponse
    {
        public CreateCourseResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public CreateCourseResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
