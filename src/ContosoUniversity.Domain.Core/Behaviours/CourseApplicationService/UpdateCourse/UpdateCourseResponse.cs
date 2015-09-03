namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class UpdateCourseResponse : DomainResponse
    {
        public UpdateCourseResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public UpdateCourseResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
