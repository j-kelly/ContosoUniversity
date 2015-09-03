namespace ContosoUniversity.Domain.Core.Behaviours
{
    using DAL;
    using System.Linq;
    using Models;
    using NRepository.Core;
    using NRepository.EntityFramework;
    using ContosoUniversity.Core.Domain.ContextualValidation;
    using InstructorApplicationService.CreateInstructorWithCourses;


    /*
         ************************************************************************************************
         * Place this in the ContosoUniversity.Domain.AppServices.Tests/xxxxxApplicationServiceTests folder 
         * **********************************************************************************************
        [TestFixture]
        public class CreateInstructorWithCoursesTests 
        {
			public CreateInstructorWithCoursesRequest CreateValidRequest(params Action<CreateInstructorWithCoursesRequest>[] updates)
	        {
				var commandModel = new CreateInstructorWithCoursesCommandModel();
				var request = new CreateInstructorWithCoursesRequest("UserId", commandModel);
				updates.ToList().ForEach(func => func(request));
				return request;
	        }

            [Test]
            public void CheckInvariantValidation()
            {
                Action<CreateInstructorWithCoursesRequest> CallSut = request =>
                {
                    var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                    serviceUnderTest.CreateInstructorWithCourses(request);
                };

                Assert2.CheckInvariants("Command cannot be null", () => CallSut(CreateValidRequest(p => p.CommandModel = null)));
                Assert2.CheckInvariants("ProductId must be > 0", () => CallSut(CreateValidRequest(p => p.CommandModel.ProductId = 0)));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
                // Assert2.CheckInvariants("", () => CallSut(CreateValidRequest(p => p.CommandModel. )));
            }

            [Test]
            public void CheckValidationRules()
            {
               Func<CreateInstructorWithCoursesRequest, ValidationDetails> CallSut = request =>
               {
                  var serviceUnderTest = new ProductApplicationServiceFactory().Object;
                  var reponse = serviceUnderTest.CreateInstructorWithCourses(request);
                  return reponse.ValidationDetails;
               };

               // Assert2.CheckValidation(
               //     "The effective date must not be before today",
               //     "EffectiveFrom",
               //     () => CallSut(CreateValidRequest(p => p.CommandModel.EffectiveFrom = DateTimeHelper.Today.AddDays(-1))));           }
        
            [Test]
            public void CheckCreateInstructorWithCoursesHappyPath()
            {
            }
        }
        
    */

    // ************************************************************************************************
    // * Place this in the Domain.AppServices/xxxApplicationService/Handlers folder
    // * **********************************************************************************************  
    // Request Handler
    public class CreateInstructorWithCoursesHandler
    {
        private readonly IRepository _Repository;

        public CreateInstructorWithCoursesHandler(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateInstructorWithCoursesResponse Handle(CreateInstructorWithCoursesRequest request)
        {
            var validationDetails = Validator.ValidateRequest(request);
            if (validationDetails.HasValidationIssues)
                return new CreateInstructorWithCoursesResponse(validationDetails);

            var commandModel = request.CommandModel;
            var courses = commandModel.SelectedCourses == null
                ? new Course[0].ToList()
                :commandModel.SelectedCourses.Select(courseId =>
                {
                    var course = new Course { CourseID = courseId };
                    _Repository.UpdateEntityState(course, System.Data.Entity.EntityState.Unchanged);
                    return course;
                }).ToList();
                
            var instructor = new Instructor
            {
                HireDate = commandModel.HireDate,
                FirstMidName = commandModel.FirstMidName,
                LastName = commandModel.LastName,
                Courses = courses,
                OfficeAssignment = new OfficeAssignment { Location = commandModel.OfficeLocation },
            };

            _Repository.Add(instructor);
            validationDetails = _Repository.SaveWithValidation();
            return new CreateInstructorWithCoursesResponse(validationDetails);
        }
    }
}