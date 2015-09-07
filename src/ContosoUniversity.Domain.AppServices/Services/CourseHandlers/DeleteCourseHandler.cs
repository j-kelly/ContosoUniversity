namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Factories;
    using NRepository.Core;

    [GenerateTestFactory]
    public class DeleteCourseHandler
    {
        private readonly IRepository _Repository;

        public DeleteCourseHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteCourse.Response Handle(DeleteCourse.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new DeleteCourse.Response(validationDetails);

            var course = CourseFactory.CreatePartial(request.CommandModel.CourseId);
            var container = course.Delete();
            _Repository.Save(container);

            return new DeleteCourse.Response();
        }
    }
}
