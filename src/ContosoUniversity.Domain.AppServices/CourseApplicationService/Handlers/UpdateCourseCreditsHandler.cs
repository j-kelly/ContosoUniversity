namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using CourseApplicationService.UpdateCourseCredits;
    using NRepository.Core;
    using NRepository.EntityFramework;


    /*
             ************************************************************************************************
             * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
             * **********************************************************************************************
            [TestFixture]
            public class UpdateCourseCreditsTests 
            {
    			public UpdateCourseCreditsRequest CreateValidRequest(params Action<UpdateCourseCreditsRequest>[] updates)
    	        {
    				var commandModel = new UpdateCourseCreditsCommandModel();
    				var request = new UpdateCourseCreditsRequest("UserId", commandModel);
    				updates.ToList().ForEach(func => func(request));
    				return request;
    	        }

                [Test]
                public void CheckInvariantValidation()
                {
                    Action<UpdateCourseCreditsRequest> CallSut = request =>
                    {
                        var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                        serviceUnderTest.UpdateCourseCredits(request);
                    };

                    Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                    Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                    // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                    // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                }

                [Test]
                public void CheckValidationRules()
                {
                   Func<UpdateCourseCreditsRequest, ValidationDetails> CallSut = request =>
                   {
                      var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                      var reponse = serviceUnderTest.UpdateCourseCredits(request);
                      return reponse.ValidationDetails;
                   };

                   // Assert2.CheckValidation(
                   //     "The effective date must not be before today",
                   //     "EffectiveFrom",
                   //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
            
                [Test]
                public void CheckUpdateCourseCreditsHappyPath()
                {
                }
            }

       
        */

    public class UpdateCourseCreditsHandler
    {
        private readonly IRepository _Repository;

        public UpdateCourseCreditsHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateCourseCreditsResponse Handle(UpdateCourseCreditsRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new UpdateCourseCreditsResponse(validationDetails);

            var rowsAffected = _Repository.ExecuteStoredProcudure(
                "UPDATE Course SET Credits = Credits * {0}",
                request.CommandModel.Multiplier);

            return new UpdateCourseCreditsResponse(rowsAffected);
        }
    }
}
