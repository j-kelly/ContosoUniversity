namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Factories;
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

            var container = StudentFactory
                .CreatePartial(request.CommandModel.StudentId)
                .Delete();

            validationDetails = _Repository.Save(container);

            return new DeleteStudent.Response(validationDetails);
        }
    }
}