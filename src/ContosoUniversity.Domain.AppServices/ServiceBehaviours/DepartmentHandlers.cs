namespace ContosoUniversity.Domain.AppServices.ServiceBehaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using ContosoUniversity.Domain.Core.Behaviours.Departments;
    using Core.Factories;
    using Core.Repository.Entities;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public static class DepartmentHandlers
    {
        public static DepartmentCreate.Response Handle(IRepository repository, DepartmentCreate.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request, repository);
            if (validationDetails.HasValidationIssues)
                return new DepartmentCreate.Response(validationDetails);

            var container = DepartmentFactory.Create(request.CommandModel);
            validationDetails = repository.Save(container);

            var deptId = default(int?);
            if (!validationDetails.HasValidationIssues)
                deptId = container.FindEntity<Department>().DepartmentID;

            return new DepartmentCreate.Response(validationDetails, deptId);
        }

        public static DepartmentDelete.Response Handle(IRepository repository, DepartmentDelete.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request, repository);
            if (validationDetails.HasValidationIssues)
                return new DepartmentDelete.Response(validationDetails);

            var department = repository.GetEntity<Department>(
                p => p.DepartmentID == request.CommandModel.DepartmentID,
                new EagerLoadingQueryStrategy<Department>(
                    p => p.Administrator));

            var hasConcurrencyError = false;
            var container = department.Delete();
            validationDetails = repository.Save(container, dbUpdateConcurrencyExceptionFunc: dbUpdateEx =>
            {
                hasConcurrencyError = true;
                return new ValidationMessageCollection(new ValidationMessage(string.Empty, dbUpdateEx.ToString()));
            });

            return new DepartmentDelete.Response(validationDetails, hasConcurrencyError);
        }

        #region UpdateDepartment Handler
        public static DepartmentUpdate.Response Handle(IRepository repository, DepartmentUpdate.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request, repository);
            if (validationDetails.HasValidationIssues)
                return new DepartmentUpdate.Response(validationDetails);

            var commandModel = request.CommandModel;
            var currentDept = repository.GetEntity<Department>(
                p => p.DepartmentID == commandModel.DepartmentID,
                new AsNoTrackingQueryStrategy());

            var rowVersion = default(byte[]);
            var container = currentDept.Modify(request.CommandModel);
            validationDetails = repository.Save(container, dbUpdateEx => OnUpdateFailedFunc(repository, dbUpdateEx, commandModel, ref rowVersion));

            return new DepartmentUpdate.Response(validationDetails, rowVersion);
        }

        private static ValidationMessageCollection OnUpdateFailedFunc(IRepository repository, DbUpdateConcurrencyException dbUpdateEx, DepartmentUpdate.CommandModel commandModel, ref byte[] rowVersion)
        {
            var validationMessages = new ValidationMessageCollection();

            var entry = dbUpdateEx.Entries.Single();
            var databaseEntry = entry.GetDatabaseValues();
            if (databaseEntry == null)
            {
                validationMessages.Add(string.Empty, "Unable to save changes. The department was deleted by another user.");
                return validationMessages;
            }

            var databaseValues = (Department)databaseEntry.ToObject();
            rowVersion = databaseValues.RowVersion;

            if (databaseValues.Name != commandModel.Name)
                validationMessages.Add(nameof(commandModel.Name), "Current value: " + databaseValues.Name);

            if (databaseValues.Budget != commandModel.Budget)
                validationMessages.Add(nameof(commandModel.Budget), "Current value: " + string.Format("{0:c}", databaseValues.Budget));

            if (databaseValues.StartDate != commandModel.StartDate)
                validationMessages.Add(nameof(commandModel.StartDate), "Current value: " + string.Format("{0:d}", databaseValues.StartDate));

            if (databaseValues.InstructorID != commandModel.InstructorID)
                validationMessages.Add(nameof(commandModel.InstructorID), "Current value: " + repository.GetEntity<Instructor>(p => p.ID == databaseValues.InstructorID.Value).FullName);

            validationMessages.Add(string.Empty, "The record you attempted to edit "
                + "was modified by another user after you got the original value. The "
                + "edit operation was canceled and the current values in the database "
                + "have been displayed. If you still want to edit this record, click "
                + "the Save button again. Otherwise click the Back to List hyperlink.");

            return validationMessages;
        }
        #endregion
    }
}