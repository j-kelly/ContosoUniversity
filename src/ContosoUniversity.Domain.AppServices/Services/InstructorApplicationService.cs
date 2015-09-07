namespace ContosoUniversity.Domain.Core.Behaviours.InstructorApplicationService
{
    using ContosoUniversity.Core.Logging;
    using NRepository.Core;
    using Utility.Logging;

    public class InstructorApplicationService : IInstructorApplicationService
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(InstructorApplicationService));

        private readonly IRepository _Repository;

        public InstructorApplicationService(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateInstructorWithCourses.Response CreateInstructorWithCourses(CreateInstructorWithCourses.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new CreateInstructorWithCoursesHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteInstructor.Response DeleteInstructor(DeleteInstructor.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new DeleteInstructorHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public ModifyInstructorAndCourses.Response ModifyInstructorAndCourses(ModifyInstructorAndCourses.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new ModifyInstructorAndCoursesHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
