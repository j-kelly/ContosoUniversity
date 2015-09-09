namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Factories;
    using InstructorApplicationService;
    using NRepository.Core;
    using Repository.Entities;

    [GenerateTestFactory]
    public class CreateInstructorWithCoursesHandler
    {
        private readonly IRepository _Repository;

        public CreateInstructorWithCoursesHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateInstructorWithCourses.Response Handle(CreateInstructorWithCourses.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateInstructorWithCourses.Response(validationDetails);

            var container = InstructorFactory.Create(_Repository, request.CommandModel);
            validationDetails = _Repository.Save(container);

            var instructorId = default(int?);
            if (!validationDetails.HasValidationIssues)
                instructorId = container.FindEntity<Instructor>().ID;

            return new CreateInstructorWithCourses.Response(validationDetails, instructorId);
        }
    }
}