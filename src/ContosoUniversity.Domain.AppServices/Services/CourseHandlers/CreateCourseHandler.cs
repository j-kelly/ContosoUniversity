namespace ContosoUniversity.Domain.Services.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService;
    using Core.Factories;
    using NRepository.Core;

    [GenerateTestFactory]
    public class CreateCourseHandler
    {
        private readonly IRepository _Repository;

        public CreateCourseHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateCourse.Response Handle(CreateCourse.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateCourse.Response(validationDetails);

            var container = CourseFactory.Create(request.CommandModel);
            validationDetails = _Repository.Save(container);

            return new CreateCourse.Response(validationDetails);
        }
    }
}