namespace ContosoUniversity.Domain.Services.CourseApplicationService.Handlers
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService.UpdateCourse;
    using Models;
    using NRepository.Core;

    public class UpdateCourseHandler
    {
        private readonly IRepository _Repository;

        public UpdateCourseHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateCourseResponse Handle(UpdateCourseRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new UpdateCourseResponse(validationDetails);

            var course = new Course
            {
                CourseID = request.CommandModel.CourseID,
                DepartmentID = request.CommandModel.DepartmentID,
                Credits = request.CommandModel.Credits,
                Title = request.CommandModel.Title,
            };

            _Repository.Modify(course);
            validationDetails = _Repository.SaveWithValidation();

            return new UpdateCourseResponse(validationDetails);
        }
    }
}
