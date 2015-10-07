namespace ContosoUniversity.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    public static class DomainServices
    {
        private static Dictionary<Type, Func<IDomainRequest, IDomainResponse>> _Handlers = new Dictionary<Type, Func<IDomainRequest, IDomainResponse>>();

        [DebuggerStepThrough]
        public static T CallService<T>(IDomainRequest request) where T : IDomainResponse
        {
            return (T)_Handlers[request.GetType()](request);
        }

        [DebuggerStepThrough]
        public static async Task<T> CallServiceAsync<T>(IDomainRequest request) where T : IDomainResponse
        {
            return await Task.Run(() => CallService<T>(request));;
        }

        [DebuggerStepThrough]
        public static IDomainResponse CallService(IDomainRequest request)
        {
            return _Handlers[request.GetType()](request);
        }

        [DebuggerStepThrough]
        public static async Task<IDomainResponse> CallServiceAsync(IDomainRequest request)
        {
            return await Task.Run(() => CallService(request)); ;
        }


        public static void AddService<T>(Func<T, IDomainResponse> func) where T : class, IDomainRequest
        {
            // wrap it up so it builds
            Func<IDomainRequest, IDomainResponse> wrappedFunc = p => func((T)p);
            _Handlers.Add(typeof(T), wrappedFunc);
        }
    }
}
