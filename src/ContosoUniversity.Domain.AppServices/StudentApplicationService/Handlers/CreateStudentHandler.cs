namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
    using NRepository.Core;
    using StudentApplicationService.CreateStudent;


    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class CreateStudentTests 
        {
			public CreateStudentRequest CreateValidRequest(params Action<CreateStudentRequest>[] updates)
	        {
				var commandModel = new CreateStudentCommandModel();
				var request = new CreateStudentRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<CreateStudentRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.CreateStudent(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<CreateStudentRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.CreateStudent(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckCreateStudentHappyPath()
            {
            }
        }

         ************************************************************************************************
         * Place this in the IxxxxApplicationService class 
         * **********************************************************************************************   
        // CreateStudentResponse CreateStudent(CreateStudentRequest request);

     
         ************************************************************************************************
         * Place this in the xxxxApplicationService class 
         * **********************************************************************************************  
        public CreateStudentResponse CreateStudent(CreateStudentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<CreateStudentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

   
    */

    public class CreateStudentHandler
    {
        private readonly IRepository _Repository;

        public CreateStudentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateStudentResponse Handle(CreateStudentRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateStudentResponse(validationDetails);

            var commandModel = request.CommandModel;
            var student = new Student
            {
                EnrollmentDate = commandModel.EnrollmentDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
            };

            _Repository.Add(student);
            validationDetails = _Repository.SaveWithValidation();

            return new CreateStudentResponse(validationDetails);
        }
    }
}