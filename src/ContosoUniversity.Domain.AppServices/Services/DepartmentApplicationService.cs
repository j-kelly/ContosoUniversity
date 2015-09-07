namespace ContosoUniversity.Domain.Services.DepartmentApplicationService
{
    using ContosoUniversity.Core.Logging;
    using ContosoUniversity.Domain.Core.Behaviours;
    using Core.Behaviours.DepartmentApplicationService;
    using NRepository.Core;
    using Utility.Logging;

    public class DepartmentApplicationService : IDepartmentApplicationService
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(DepartmentApplicationService));

        private readonly IRepository _Repository;

        public DepartmentApplicationService(IRepository repository)
        {
            _Repository = repository;
        }

        public UpdateDepartment.Response UpdateDepartment(UpdateDepartment.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new UpdateDepartmentHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public CreateDepartment.Response CreateDepartment(CreateDepartment.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new CreateDepartmentHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteDepartment.Response DeleteDepartment(DeleteDepartment.Request request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = new DeleteDepartmentHandler(_Repository);
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
