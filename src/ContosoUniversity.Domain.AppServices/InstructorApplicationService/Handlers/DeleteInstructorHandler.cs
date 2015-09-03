namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using InstructorApplicationService.DeleteInstructor;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;


    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class DeleteInstructorTests 
        {
			public DeleteInstructorRequest CreateValidRequest(params Action<DeleteInstructorRequest>[] updates)
	        {
				var commandModel = new DeleteInstructorCommandModel();
				var request = new DeleteInstructorRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<DeleteInstructorRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.DeleteInstructor(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<DeleteInstructorRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.DeleteInstructor(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckDeleteInstructorHappyPath()
            {
            }
        }
    */

    public class DeleteInstructorHandler
    {
        private readonly IRepository _Repository;

        public DeleteInstructorHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public DeleteInstructorResponse Handle(DeleteInstructorRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new DeleteInstructorResponse(validationDetails);

            var depts = _Repository.GetEntities<Department>(p => p.InstructorID == request.CommandModel.InstructorId);
            foreach (var dept in depts)
            {
                dept.InstructorID = null;
                _Repository.Modify(dept);
            }

            var deletedInstructor = _Repository.GetEntity<Instructor>(
                p => p.ID == request.CommandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.OfficeAssignment));

            deletedInstructor.OfficeAssignment = null;

            _Repository.Delete(deletedInstructor);
            validationDetails = _Repository.SaveWithValidation();

            return new DeleteInstructorResponse(validationDetails);
        }
    }
}