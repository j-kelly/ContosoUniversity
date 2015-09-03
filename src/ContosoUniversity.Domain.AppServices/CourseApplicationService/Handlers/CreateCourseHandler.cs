namespace ContosoUniversity.Domain.Services.CourseApplicationService.Handlers
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using Core.Behaviours.CourseApplicationService.CreateCourse;
    using Models;
    using NRepository.Core;

    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class InsertCourseTests 
        {
			public InsertCourseRequest CreateValidRequest(params Action<InsertCourseRequest>[] updates)
	        {
				var commandModel = new InsertCourseCommandModel();
				var request = new InsertCourseRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<InsertCourseRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.InsertCourse(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<InsertCourseRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.InsertCourse(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckInsertCourseHappyPath()
            {
            }
        }
    */

    public class CreateCourseHandler
    {
        private readonly IRepository _Repository;

        public CreateCourseHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateCourseResponse Handle(CreateCourseRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateCourseResponse(validationDetails);

            var course = new Course
            {
                CourseID = request.CommandModel.CourseID,
                DepartmentID = request.CommandModel.DepartmentID,
                Title = request.CommandModel.Title,
                Credits = request.CommandModel.Credits
            };

            _Repository.Add(course);
            validationDetails = _Repository.SaveWithValidation();

            return new CreateCourseResponse(validationDetails);
        }
    }
}