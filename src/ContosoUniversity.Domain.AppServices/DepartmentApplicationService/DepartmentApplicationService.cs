namespace ContosoUniversity.Domain.Services.DepartmentApplicationService
{
    using ContosoUniversity.Core.Logging;
    using ContosoUniversity.Domain.Core.Behaviours;
    using Core.Behaviours.DepartmentApplicationService;
    using Core.Behaviours.DepartmentApplicationService.CreateDepartment;
    using Core.Behaviours.DepartmentApplicationService.DeleteDepartment;
    using Core.Behaviours.DepartmentApplicationService.UpdateDepartment;
    using Microsoft.Practices.Unity;
    using Utility.Logging;

    public class DepartmentApplicationService : IDepartmentApplicationService
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(ContosoUniversity.Domain.Services.DepartmentApplicationService.DepartmentApplicationService));

        private readonly IUnityContainer _UnityContainer;

        public DepartmentApplicationService(IUnityContainer unityContainer)
        {
            _UnityContainer = unityContainer;
        }

        public UpdateDepartmentResponse UpdateDepartment(UpdateDepartmentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<UpdateDepartmentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public CreateDepartmentResponse CreateDepartment(CreateDepartmentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<CreateDepartmentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }

        public DeleteDepartmentResponse DeleteDepartment(DeleteDepartmentRequest request)
        {
            var retVal = Logger.TraceCall(() =>
            {
                var requestHandler = _UnityContainer.Resolve<DeleteDepartmentHandler>();
                var response = requestHandler.Handle(request);
                return response;
            });

            return retVal;
        }
    }
}
