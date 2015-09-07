namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
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

            var commandModel = request.CommandModel;
            var student = new Student
            {
                EnrollmentDate = commandModel.EnrollmentDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
            };

            _Repository.Add(student);
            validationDetails = _Repository.SaveWithValidation();

            return new CreateStudent.Response(validationDetails);
        }
    }
}