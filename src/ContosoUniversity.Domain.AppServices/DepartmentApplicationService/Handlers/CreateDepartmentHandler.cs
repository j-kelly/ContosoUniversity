namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DepartmentApplicationService.CreateDepartment;
    using Models;
    using NRepository.Core;


    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class InsertDepartmentTests 
        {
			public InsertDepartmentRequest CreateValidRequest(params Action<InsertDepartmentRequest>[] updates)
	        {
				var commandModel = new InsertDepartmentCommandModel();
				var request = new InsertDepartmentRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<InsertDepartmentRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.InsertDepartment(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<InsertDepartmentRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.InsertDepartment(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckInsertDepartmentHappyPath()
            {
            }
        }
    */

    public class CreateDepartmentHandler
    {
        private readonly IRepository _Repository;

        public CreateDepartmentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateDepartmentResponse Handle(CreateDepartmentRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request, _Repository);
            if (validationDetails.HasValidationIssues)
                return new CreateDepartmentResponse(validationDetails);

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

            return new CreateDepartmentResponse(validationDetails);
        }
    }
}