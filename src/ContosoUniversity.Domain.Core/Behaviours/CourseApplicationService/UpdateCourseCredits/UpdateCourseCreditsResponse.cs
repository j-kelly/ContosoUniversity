namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourseCredits
{
    using ContosoUniversity.Core.Domain;
    using ContosoUniversity.Core.Domain.ContextualValidation;

    public class UpdateCourseCreditsResponse : DomainResponse
    {
        public UpdateCourseCreditsResponse(int rowsEffected)
            : base(new ValidationMessageCollection())
        {
            RowsEffected = rowsEffected;
        }

        public UpdateCourseCreditsResponse(ValidationMessageCollection validationDetails)
            : base(validationDetails)
        {
        }

        public int? RowsEffected
        {
            get;
        }
    }
}
