namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.ModifyInstructorAndCourses
{
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class ModifyInstructorAndCoursesResponse : ContosoUniversity.Core.Domain.DomainResponse
    {
        public ModifyInstructorAndCoursesResponse()
            : base(new ValidationMessageCollection())
        {
        }

        public ModifyInstructorAndCoursesResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }
    }
}
