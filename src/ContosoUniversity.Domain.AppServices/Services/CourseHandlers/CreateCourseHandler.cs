namespace ContosoUniversity.Domain.Services.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService;
    using Models;
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

            var course = new Course
            {
                CourseID = request.CommandModel.CourseID,
                DepartmentID = request.CommandModel.DepartmentID,
                Title = request.CommandModel.Title,
                Credits = request.CommandModel.Credits
            };

            _Repository.Add(course);
            validationDetails = _Repository.SaveWithValidation();

            return new CreateCourse.Response(validationDetails);
        }
    }
}