namespace ContosoUniversity.Domain.Core.Behaviours.StudentApplicationService
{
    using ContosoUniversity.Core.Logging;
    using NRepository.Core;
    using Utility.Logging;

    public class StudentApplicationService : IStudentApplicationService
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(StudentApplicationService));
        private readonly IRepository _Repository;

        public StudentApplicationService(IRepository repository)
        {
            _Repository = repository;
        }

        public CreateStudent.Response CreateStudent(CreateStudent.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new CreateStudentHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteStudent.Response DeleteStudent(DeleteStudent.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new DeleteStudentHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public ModifyStudent.Response ModifyStudent(ModifyStudent.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new ModifyStudentHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
