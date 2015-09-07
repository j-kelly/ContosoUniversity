namespace ContosoUniversity.Domain.Services.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService;
    using Core.Factories;
    using NRepository.Core;

    [GenerateTestFactory]
    public class UpdateCourseHandler
    {
        private readonly IRepository _Repository;

        public UpdateCourseHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateCourse.Response Handle(UpdateCourse.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new UpdateCourse.Response(validationDetails);

            var container = CourseFactory.CreatePartial(request.CommandModel.CourseID).Modify(request.CommandModel);
            validationDetails = _Repository.Save(container);

            return new UpdateCourse.Response(validationDetails);
        }
    }
}
