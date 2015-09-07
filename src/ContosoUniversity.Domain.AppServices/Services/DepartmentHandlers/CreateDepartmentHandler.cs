namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService;
    using Models;
    using NRepository.Core;

    [GenerateTestFactory]
    public class CreateDepartmentHandler
    {
        private readonly IRepository _Repository;

        public CreateDepartmentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateDepartment.Response Handle(CreateDepartment.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request, _Repository);
            if (validationDetails.HasValidationIssues)
                return new CreateDepartment.Response(validationDetails);

            var commandModel = request.CommandModel;
            var dept = new Department
            {
                Budget = commandModel.Budget,
                InstructorID = commandModel.InstructorID,
                Name = commandModel.Name,
                StartDate = commandModel.StartDate
            };

            _Repository.Add(dept);
            validationDetails = _Repository.SaveWithValidation();

            return new CreateDepartment.Response(validationDetails);
        }
    }
}