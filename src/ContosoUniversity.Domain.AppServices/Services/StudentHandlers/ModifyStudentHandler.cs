namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
    using NRepository.Core;
    using StudentApplicationService;

    [GenerateTestFactory]
    public class ModifyStudentHandler
    {
        private readonly IRepository _Repository;

        public ModifyStudentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public ModifyStudent.Response Handle(ModifyStudent.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new ModifyStudent.Response(validationDetails);

            var commandModel = request.CommandModel;
            var student = new Student
            {
                ID = commandModel.ID,
                EnrollmentDate = commandModel.EnrollmentDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
            };

            _Repository.Modify(student);
            validationDetails = _Repository.SaveWithValidation();

            return new ModifyStudent.Response(validationDetails);
        }
    }
}