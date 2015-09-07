namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Factories;
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
            var container = StudentFactory.CreatePartial(commandModel.ID).Modify(commandModel);
            validationDetails = _Repository.Save(container);

            return new ModifyStudent.Response(validationDetails);
        }
    }
}