namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using CourseApplicationService.DeleteCourse;
    using Models;
    using NRepository.Core;


    /*
             ************************************************************************************************
             * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
             * **********************************************************************************************
            [TestFixture]
            public class DeleteCourseTests 
            {
    			public DeleteCourseRequest CreateValidRequest(params Action<DeleteCourseRequest>[] updates)
    	        {
    				var commandModel = new DeleteCourseCommandModel();
    				var request = new DeleteCourseRequest("UserId", commandModel);
    				updates.ToList().ForEach(func => func(request));
    				return request;
    	        }

                [Test]
                public void CheckInvariantValidation()
                {
                    Action<DeleteCourseRequest> CallSut = request =>
                    {
                        var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                        serviceUnderTest.DeleteCourse(request);
                    };

                    Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                    Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                    // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                    // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                }

                [Test]
                public void CheckValidationRules()
                {
                   Func<DeleteCourseRequest, ValidationDetails> CallSut = request =>
                   {
                      var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                      var reponse = serviceUnderTest.DeleteCourse(request);
                      return reponse.ValidationDetails;
                   };

                   // Assert2.CheckValidation(
                   //     "The effective date must not be before today",
                   //     "EffectiveFrom",
                   //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
            
                [Test]
                public void CheckDeleteCourseHappyPath()
                {
                }
            }
       
        */

    public class DeleteCourseHandler
    {
        private readonly IRepository _Repository;

        public DeleteCourseHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteCourseResponse Handle(DeleteCourseRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new DeleteCourseResponse(validationDetails);

            var course = new Course { CourseID = request.CommandModel.CourseId };
            _Repository.Modify(course);
            _Repository.Delete(course);
            _Repository.Save();

            return new DeleteCourseResponse();
        }
    }
}
