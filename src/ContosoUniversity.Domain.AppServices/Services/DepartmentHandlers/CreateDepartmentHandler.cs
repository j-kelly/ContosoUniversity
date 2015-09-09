namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService;
    using Factories;
    using NRepository.Core;
    using Repository.Entities;

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

            var container = DepartmentFactory.Create(request.CommandModel);
            validationDetails = _Repository.Save(container);

            var deptId = default(int?);
            if (!validationDetails.HasValidationIssues)
                deptId = container.FindEntity<Department>().DepartmentID;

            return new CreateDepartment.Response(validationDetails, deptId);
        }
    }
}