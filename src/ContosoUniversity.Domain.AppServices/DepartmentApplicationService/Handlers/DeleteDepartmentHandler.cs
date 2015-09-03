namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService.DeleteDepartment;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;

    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class DeleteDepartmentTests 
        {
			public DeleteDepartmentRequest CreateValidRequest(params Action<DeleteDepartmentRequest>[] updates)
	        {
				var commandModel = new DeleteDepartmentCommandModel();
				var request = new DeleteDepartmentRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<DeleteDepartmentRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.DeleteDepartment(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<DeleteDepartmentRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.DeleteDepartment(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckDeleteDepartmentHappyPath()
            {
            }
        }

         ************************************************************************************************
         * Place this in the IxxxxApplicationService class 
         * **********************************************************************************************   
        // DeleteDepartmentResponse DeleteDepartment(DeleteDepartmentRequest request);

     
         ************************************************************************************************
         * Place this in the xxxxApplicationService class 
         * **********************************************************************************************  
        public DeleteDepartmentResponse DeleteDepartment(DeleteDepartmentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<DeleteDepartmentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

   
    */

    public class DeleteDepartmentHandler
    {
        private readonly IRepository _Repository;

        public DeleteDepartmentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteDepartmentResponse Handle(DeleteDepartmentRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request, _Repository);
            if (validationDetails.HasValidationIssues)
                return new DeleteDepartmentResponse(validationDetails);

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

            return new DeleteDepartmentResponse(validationDetails, hasConcurrencyError);
        }
    }
}