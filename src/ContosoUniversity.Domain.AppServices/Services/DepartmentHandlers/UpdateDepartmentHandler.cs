namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Annotations;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    [GenerateTestFactory]
    public class UpdateDepartmentHandler
    {
        private readonly IRepository _Repository;

        public UpdateDepartmentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateDepartment.Response Handle(UpdateDepartment.Request request)
        {
            var validationDetails = Validator.ValidateRequest(request, _Repository);
            if (validationDetails.HasValidationIssues)
                return new UpdateDepartment.Response(validationDetails);

            var commandModel = request.CommandModel;
            var currentDept = _Repository.GetEntity<Department>(
                p => p.DepartmentID == commandModel.DepartmentID,
                new AsNoTrackingQueryStrategy());

            var dept = new Department
            {
                DepartmentID = commandModel.DepartmentID,
                Budget = commandModel.Budget,
                InstructorID = commandModel.InstructorID,
                Name = commandModel.Name,
                RowVersion = commandModel.RowVersion,
                StartDate = commandModel.StartDate,
                CreatedBy = currentDept.CreatedBy,
                CreatedOn = currentDept.CreatedOn
            };

            var rowVersion = default(byte[]);

            _Repository.Modify(dept);
            validationDetails = _Repository.SaveWithValidation(dbUpdateEx => OnUpdateFailedFunc(dbUpdateEx, commandModel, ref rowVersion));

            return new UpdateDepartment.Response(validationDetails, rowVersion);
        }

        private ValidationMessageCollection OnUpdateFailedFunc(DbUpdateConcurrencyException dbUpdateEx, UpdateDepartment.CommandModel commandModel, ref byte[] rowVersion)
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
                validationMessages.Add(nameof(commandModel.InstructorID), "Current value: " + _Repository.GetEntity<Instructor>(p => p.ID == databaseValues.InstructorID.Value).FullName);

            validationMessages.Add(string.Empty, "The record you attempted to edit "
                + "was modified by another user after you got the original value. The "
                + "edit operation was canceled and the current values in the database "
                + "have been displayed. If you still want to edit this record, click "
                + "the Save button again. Otherwise click the Back to List hyperlink.");

            return validationMessages;
        }
    }
}