namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;

    [GenerateTestFactory]
    public class DeleteDepartmentHandler
    {
        private readonly IRepository _Repository;

        public DeleteDepartmentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteDepartment.Response Handle(DeleteDepartment.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request, _Repository);
            if (validationDetails.HasValidationIssues)
                return new DeleteDepartment.Response(validationDetails);

            var department = _Repository.GetEntity<Department>(
                p => p.DepartmentID == request.CommandModel.DepartmentID,
                new EagerLoadingQueryStrategy<Department>(
                    p => p.Administrator));

            department.Administrator = null;
            _Repository.Delete(department);

            var hasConcurrencyError = false;
            validationDetails = _Repository.SaveWithValidation(dbUpdateConcurrencyExceptionFunc: dbUpdateEx =>
            {
                hasConcurrencyError = true;
                return new ValidationMessageCollection(new ValidationMessage(string.Empty, dbUpdateEx.ToString()));
            });

            return new DeleteDepartment.Response(validationDetails, hasConcurrencyError);
        }
    }
}