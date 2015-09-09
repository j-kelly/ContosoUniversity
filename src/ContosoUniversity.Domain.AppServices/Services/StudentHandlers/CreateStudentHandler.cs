namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Repository.Entities;
    using Factories;
    using NRepository.Core;
    using StudentApplicationService;

    [GenerateTestFactory]
    public class CreateStudentHandler
    {
        private readonly IRepository _Repository;

        public CreateStudentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateStudent.Response Handle(CreateStudent.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateStudent.Response(validationDetails);

            var container = StudentFactory.Create(request.CommandModel);
            validationDetails = _Repository.Save(container);

            var studentId = default(int?);
            if (!validationDetails.HasValidationIssues)
                studentId = container.FindEntity<Student>().ID;

            return new CreateStudent.Response(validationDetails, studentId);
        }
    }
}