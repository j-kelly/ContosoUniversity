namespace ContosoUniversity.Domain.Services.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService;
    using Models;
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

            var course = new Course
            {
                CourseID = request.CommandModel.CourseID,
                DepartmentID = request.CommandModel.DepartmentID,
                Credits = request.CommandModel.Credits,
                Title = request.CommandModel.Title,
            };

            _Repository.Modify(course);
            validationDetails = _Repository.SaveWithValidation();

            return new UpdateCourse.Response(validationDetails);
        }
    }
}
