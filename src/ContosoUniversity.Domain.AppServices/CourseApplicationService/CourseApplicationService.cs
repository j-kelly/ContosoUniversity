namespace ContosoUniversity.Domain.AppServices
{
    using ContosoUniversity.Core.Logging;
    using Core.Behaviours;
    using Core.Behaviours.CourseApplicationService;
    using Core.Behaviours.CourseApplicationService.CreateCourse;
    using Core.Behaviours.CourseApplicationService.DeleteCourse;
    using Core.Behaviours.CourseApplicationService.UpdateCourse;
    using Core.Behaviours.CourseApplicationService.UpdateCourseCredits;
    using Microsoft.Practices.Unity;
    using Services.CourseApplicationService.Handlers;
    using Utility.Logging;

    public class CourseApplicationService : ICourseApplicationService
    {
        private ILogger Logger = LogManager.CreateLogger(typeof(CourseApplicationService));

        private readonly IUnityContainer _UnityContainer;

        public CourseApplicationService(IUnityContainer unityContainer)
        {
            _UnityContainer = unityContainer;
        }

        public UpdateCourseResponse UpdateCourse(UpdateCourseRequest request)
        {
            var retVal = Logger.TraceCall(1, () =>
            {
                var requestHandler = _UnityContainer.Resolve<UpdateCourseHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public CreateCourseResponse CreateCourse(CreateCourseRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<CreateCourseHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteCourseResponse DeleteCourse(DeleteCourseRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<DeleteCourseHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public UpdateCourseCreditsResponse UpdateCourseCredits(UpdateCourseCreditsRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<UpdateCourseCreditsHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
