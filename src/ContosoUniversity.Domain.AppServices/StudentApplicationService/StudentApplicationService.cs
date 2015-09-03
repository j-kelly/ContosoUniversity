namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService
{
    using ContosoUniversity.Core.Logging;
    using CreateStudent;
    using DeleteStudent;
    using Microsoft.Practices.Unity;
    using ModifyStudent;
    using Utility.Logging;

    public class StudentApplicationService : IStudentApplicationService
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(StudentApplicationService));

        private readonly IUnityContainer _UnityContainer;

        public StudentApplicationService(IUnityContainer unityContainer)
        {
            _UnityContainer = unityContainer;
        }

        public CreateStudentResponse CreateStudent(CreateStudentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<CreateStudentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteStudentResponse DeleteStudent(DeleteStudentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<DeleteStudentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public ModifyStudentResponse ModifyStudent(ModifyStudentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<ModifyStudentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
