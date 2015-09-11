namespace ContosoUniversity.Domain.AppServices
{
    using ContosoUniversity.Core.Logging;
    using Core.Behaviours.CourseApplicationService;
    using NRepository.Core;
    using Services.CourseApplicationService;
    using Utility.Logging;

    public class CourseApplicationService : ICourseApplicationService
    {
        private ILogger Logger = LogManager.CreateLogger(typeof(CourseApplicationService));

        private readonly IRepository _Repository;

        public CourseApplicationService(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateCourse.Response UpdateCourse(UpdateCourse.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new UpdateCourseHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public CreateCourse.Response CreateCourse(CreateCourse.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new CreateCourseHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteCourse.Response DeleteCourse(DeleteCourse.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new DeleteCourseHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public UpdateCourseCredits.Response UpdateCourseCredits(UpdateCourseCredits.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new UpdateCourseCreditsHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
