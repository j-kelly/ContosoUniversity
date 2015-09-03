namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.DeleteCourse
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class DeleteCourseResponse : DomainResponse
    {
        public DeleteCourseResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public DeleteCourseResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
