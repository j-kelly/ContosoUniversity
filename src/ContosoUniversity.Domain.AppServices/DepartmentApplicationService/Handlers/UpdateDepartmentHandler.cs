namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService.UpdateDepartment;
    using Models;
    using NRepository.Core;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class EditDepartmentTests 
        {
			public EditDepartmentRequest CreateValidRequest(params Action<EditDepartmentRequest>[] updates)
	        {
				var commandModel = new EditDepartmentCommandModel();
				var request = new EditDepartmentRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<EditDepartmentRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.EditDepartment(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<EditDepartmentRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.EditDepartment(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckEditDepartmentHappyPath()
            {
            }
        }
    */

    public class UpdateDepartmentHandler
    {
        private readonly IRepository _Repository;

        public UpdateDepartmentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateDepartmentResponse Handle(UpdateDepartmentRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request, _Repository);
            if (validationDetails.HasValidationIssues)
                return new UpdateDepartmentResponse(validationDetails);

            var commandModel = request.CommandModel;
            var dept = new Department
            {
                DepartmentID = commandModel.DepartmentID,
                Budget = commandModel.Budget,
                InstructorID = commandModel.InstructorID,
                Name = commandModel.Name,
                RowVersion = commandModel.RowVersion,
                StartDate = commandModel.StartDate
            };

            var rowVersion = default(byte[]);

            _Repository.Modify(dept);
            validationDetails = _Repository.SaveWithValidation(dbUpdateEx => OnUpdateFailedFunc(dbUpdateEx, commandModel, ref rowVersion));

            return new UpdateDepartmentResponse(validationDetails, rowVersion);
        }

        private ValidationMessageCollection OnUpdateFailedFunc(DbUpdateConcurrencyException dbUpdateEx, UpdateDepartmentCommandModel commandModel, ref byte[] rowVersion)
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