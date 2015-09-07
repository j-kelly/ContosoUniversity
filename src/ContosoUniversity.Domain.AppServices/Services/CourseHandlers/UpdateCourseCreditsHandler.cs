namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using NRepository.Core;
    using NRepository.EntityFramework;

    [GenerateTestFactory]
    public class UpdateCourseCreditsHandler
    {
        private readonly IRepository _Repository;

        public UpdateCourseCreditsHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateCourseCredits.Response Handle(UpdateCourseCredits.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new UpdateCourseCredits.Response(validationDetails);

            var rowsAffected = _Repository.ExecuteStoredProcudure(
                "UPDATE Course SET Credits = Credits * {0}",
                request.CommandModel.Multiplier);

            return new UpdateCourseCredits.Response(rowsAffected);
        }
    }
}
