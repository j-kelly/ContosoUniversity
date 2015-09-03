namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService
{
    using ContosoUniversity.Core.Logging;
    using ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService.CreateInstructorWithCourses;
    using DeleteInstructor;
    using Microsoft.Practices.Unity;
    using ModifyInstructorAndCourses;
    using Utility.Logging;
    public class InstructorApplicationService : IInstructorApplicationService
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(InstructorApplicationService));

        private readonly IUnityContainer _UnityContainer;

        public InstructorApplicationService(IUnityContainer unityContainer)
        {
            _UnityContainer = unityContainer;
        }

        public CreateInstructorWithCoursesResponse CreateInstructorWithCourses(CreateInstructorWithCoursesRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<CreateInstructorWithCoursesHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteInstructorResponse DeleteInstructor(DeleteInstructorRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<DeleteInstructorHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

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
    }
}
