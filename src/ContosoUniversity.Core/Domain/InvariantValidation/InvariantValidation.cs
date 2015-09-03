namespace ContosoUniversity.Core.Domain.InvariantValidation
{
    using ContosoUniversity.Core.Logging;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
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
            AddTypeMethods();

            Context = context;
        }

        public T Context
        {
            get;
        }

        private void AddTypeMethods()
        {
            if (!specificationMethods.ContainsKey(typeof(T)))
            {
                lock (_syncObject)
                {
                    if (!specificationMethods.ContainsKey(typeof(T)))
                    {
                        var specs = new List<MethodInfo>();
                        var bf = BindingFlags.Public | BindingFlags.Instance;
                        GetType().GetMethods(bf).ToList().ForEach(methodInfo =>
                        {
                            if (methodInfo.DeclaringType == typeof(InvariantValidation<T>))
                                return;

                            if (methodInfo.DeclaringType == typeof(object))
                                return;

                            specs.Add(methodInfo);
                        });

                        specificationMethods[typeof(T)] = specs;
                    }
                }
            }
        }

        [DebuggerStepThrough]
        protected virtual void Assert(bool result, [CallerMemberName] string errorMessage = "")
        {
            if (!result)
            {
                var splitMessage = SplitCamelCase(errorMessage);
                throw new InvariantValidationException(splitMessage);
            }
        }

        [DebuggerStepThrough]
        protected virtual void Assert<TException>(bool result, [CallerMemberName] string errorMessage = "") where TException : Exception
        {
            if (!result)
            {
                var exception = (Exception)Activator.CreateInstance(typeof(TException), errorMessage);
                throw exception;
            }
        }

        [DebuggerStepThrough]
        public virtual void Assert(params object[] dependentServices)
        {
            try
            {
                _DependentServices = dependentServices;

                specificationMethods[typeof(T)].ForEach(methodInfo =>
                {
                    methodInfo.Invoke(this, new object[0]);
                });
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

        protected TInterface ResolveService<TInterface>()
        {
            return _DependentServices.OfType<TInterface>().Single();
        }

        protected static string SplitCamelCase(string str)
        {
            var retVal = Regex.Replace(
                Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"),
                @"(\p{Ll})(\P{Ll})",
                "$1 $2");

            return retVal.ToLower();
        }
    }
}