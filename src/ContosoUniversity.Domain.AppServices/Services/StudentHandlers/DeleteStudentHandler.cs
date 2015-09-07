namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
    using NRepository.Core;
    using StudentApplicationService;

    [GenerateTestFactory]
    public class DeleteStudentHandler
    {
        private readonly IRepository _Repository;

        public DeleteStudentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteStudent.Response Handle(DeleteStudent.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new DeleteStudent.Response(validationDetails);

            var student = _Repository.GetEntity<Student>(p => p.ID == request.CommandModel.StudentId);
            _Repository.Delete(student);
            validationDetails = _Repository.SaveWithValidation();

            return new DeleteStudent.Response(validationDetails);
        }
    }
}