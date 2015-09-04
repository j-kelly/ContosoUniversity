namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Models;
    using NRepository.Core;
    using StudentApplicationService.DeleteStudent;


    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class DeleteStudentTests 
        {
			public DeleteStudentRequest CreateValidRequest(params Action<DeleteStudentRequest>[] updates)
	        {
				var commandModel = new DeleteStudentCommandModel();
				var request = new DeleteStudentRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<DeleteStudentRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.DeleteStudent(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<DeleteStudentRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.DeleteStudent(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckDeleteStudentHappyPath()
            {
            }
        }
    */

    public class DeleteStudentHandler
    {
        private readonly IRepository _Repository;

        public DeleteStudentHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteStudentResponse Handle(DeleteStudentRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new DeleteStudentResponse(validationDetails);

            var student = _Repository.GetEntity<Student>(p => p.ID == request.CommandModel.StudentId);
            _Repository.Delete(student);
            validationDetails = _Repository.SaveWithValidation();

            return new DeleteStudentResponse(validationDetails);
        }
    }
}