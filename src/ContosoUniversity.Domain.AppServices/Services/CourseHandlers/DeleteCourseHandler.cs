namespace ContosoUniversity.Domain.Core.Behaviours.CourseApplicationService
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
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

            var course = new Course { CourseID = request.CommandModel.CourseId };
            _Repository.Modify(course);
            _Repository.Delete(course);
            _Repository.Save();

            return new DeleteCourse.Response();
        }
    }
}
