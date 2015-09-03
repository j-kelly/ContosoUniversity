namespace ContosoUniversity.Domain.Core.Behaviours
{
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using DAL;
    using InstructorApplicationService.ModifyInstructorAndCourses;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework.Query;
    using System.Linq;

    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class ModifyInstructorAndCoursesTests 
        {
			public ModifyInstructorAndCoursesRequest CreateValidRequest(params Action<ModifyInstructorAndCoursesRequest>[] updates)
	        {
				var commandModel = new ModifyInstructorAndCoursesCommandModel();
				var request = new ModifyInstructorAndCoursesRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<ModifyInstructorAndCoursesRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.ModifyInstructorAndCourses(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<ModifyInstructorAndCoursesRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.ModifyInstructorAndCourses(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckModifyInstructorAndCoursesHappyPath()
            {
            }
        }

         ************************************************************************************************
         * Place this in the IxxxxApplicationService class 
         * **********************************************************************************************   
        // ModifyInstructorAndCoursesResponse ModifyInstructorAndCourses(ModifyInstructorAndCoursesRequest request);

     
         ************************************************************************************************
         * Place this in the xxxxApplicationService class 
         * **********************************************************************************************  
        public ModifyInstructorAndCoursesResponse ModifyInstructorAndCourses(ModifyInstructorAndCoursesRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<ModifyInstructorAndCoursesHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

   
    */

    // ************************************************************************************************
    // * Place this in the Domain.AppServices/xxxApplicationService/Handlers folder
    // * **********************************************************************************************  
    // Request Handler
    public class ModifyInstructorAndCoursesHandler
    {
        private readonly IRepository _Repository;

        public ModifyInstructorAndCoursesHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public ModifyInstructorAndCoursesResponse Handle(ModifyInstructorAndCoursesRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new ModifyInstructorAndCoursesResponse(validationDetails);

            var commandModel = request.CommandModel;
            var instructor = _Repository.GetEntity<Instructor>(
                p => p.ID == commandModel.InstructorId,
                new EagerLoadingQueryStrategy<Instructor>(
                    p => p.Courses,
                    p => p.OfficeAssignment));

            // Removals first
            instructor.Courses.Clear();
            if (instructor.OfficeAssignment != null && commandModel.OfficeLocation == null)
                _Repository.Delete(instructor.OfficeAssignment);

            // Update properties
            instructor.FirstMidName = commandModel.FirstMidName;
            instructor.LastName = commandModel.LastName;
            instructor.HireDate = commandModel.HireDate;
            instructor.OfficeAssignment = new OfficeAssignment { Location = commandModel.OfficeLocation };

            if (commandModel.SelectedCourses != null)
            {
                instructor.Courses = _Repository.GetEntities<Course>(
                    new FindByIdsSpecificationStrategy<Course>(p => p.CourseID, commandModel.SelectedCourses))
                    .ToList();
            }

            _Repository.Modify(instructor);
            validationDetails = _Repository.SaveWithValidation();

            return new ModifyInstructorAndCoursesResponse(validationDetails);
        }
    }
}