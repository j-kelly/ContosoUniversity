namespace ContosoUniversity.Core.Domain.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;
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
            return await Task.Run(() => CallService<T>(request)); ;
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

        public static void AddService<T>(Expression<Func<T, IDomainResponse>> func) where T : class, IDomainRequest
        {
            // wrap it up so it builds
            Func<IDomainRequest, IDomainResponse> wrappedFunc = p =>
            {
                var fun = func.Compile().Invoke((T)p);
                return fun;
            };

            _Handlers.Add(typeof(T), wrappedFunc);
        }
    }
}
