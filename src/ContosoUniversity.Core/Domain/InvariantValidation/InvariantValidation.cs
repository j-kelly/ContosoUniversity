namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    using ContosoUniversity.Core.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Utility.Logging;

    [ExcludeFromCodeCoverage]
    public abstract class InvariantValidation<T> : IInvariantValidation
    {
        private static readonly ILogger Logger = LogManager.CreateLogger(typeof(InvariantValidation<T>));

        private static Dictionary<Type, List<MethodInfo>> specificationMethods = new Dictionary<Type, List<MethodInfo>>();

        private static object _syncObject = new object();

        private static IEnumerable<object> _DependentServices = null;

        protected InvariantValidation(T context)
        {
            Context = context;
        }

        public T Context
        {
            get;
        }


        [DebuggerStepThrough]
        protected virtual void Assert(bool result, string errorMessage)
        {
            if (!result)
            {
                throw new InvariantValidationException(errorMessage);
            }
        }

        [DebuggerStepThrough]
        public virtual void StartAsserting(params object[] dependentServices)
        {
            try
            {
                _DependentServices = dependentServices;

                Validate();
                ValidateCommandModel();
            }
            catch (TargetInvocationException targetInvocationEx)
            {
                var ex = (Exception)targetInvocationEx;
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                Logger.Error(ex.ToString());
                throw ex;
            }
        }

        [DebuggerStepThrough]
        public virtual void Validate() { }

        [DebuggerStepThrough]
        public virtual void ValidateCommandModel() { }

        protected TInterface ResolveService<TInterface>()
        {
            return _DependentServices.OfType<TInterface>().Single();
        }
    }
}