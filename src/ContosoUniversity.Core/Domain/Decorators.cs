namespace ContosoUniversity.Core.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Logging;

    // Common decorators for the DomainServices
    public static class Decorators
    {
        /// <summary>
        /// Add timing logs to each call
        /// </summary>
        public static IDomainResponse Log<T>(T command, Expression<Func<T, IDomainResponse>> next) where T : class, IDomainRequest
        {
            var logger = LogManager.CreateLogger<T>();
            var retVal = logger.LogTimings(() => next.Compile().Invoke(command));
            return retVal;
        }

        /// <summary>
        /// Alters the expression so any dependencies are created separately which then allows them to be disposed correctly
        /// </summary>
        public static IDomainResponse AutoDispose<T>(Expression<Func<T, IDomainResponse>> next) where T : class, IDomainRequest
        {
            if (next.Body.NodeType != ExpressionType.Call)
                throw new NotSupportedException($"{next.NodeType} is not supported");

            var parameters = new List<object>();

            try
            {
                var bodyCallExpression = (MethodCallExpression)next.Body;
                foreach (var arg in bodyCallExpression.Arguments)
                {
                    if (arg.NodeType == ExpressionType.MemberAccess)
                    {
                        var memberExpression = (MemberExpression)arg;
                        var val = Expression.Lambda(memberExpression).Compile().DynamicInvoke();
                        parameters.Add(val);
                        continue;
                    }

                    if (arg.NodeType == ExpressionType.Invoke)
                    {
                        var callEx = (InvocationExpression)arg;
                        var val = Expression.Lambda(callEx).Compile().DynamicInvoke();
                        parameters.Add(val);
                        continue;
                    }

                    if (arg.NodeType == ExpressionType.New)
                    {
                        var newExpression = (NewExpression)arg;
                        var val = Expression.Lambda(newExpression).Compile().DynamicInvoke();
                        parameters.Add(val);
                    }
                }

                var method = bodyCallExpression.Method;
                var retVal = (IDomainResponse)method.Invoke(null, parameters.ToArray());
                return retVal;
            }
            finally
            {
                parameters.ToList().ForEach(dependency =>
                {
                    if (dependency != null && dependency is IDisposable)
                        ((IDisposable)dependency).Dispose();
                });
            }
        }
    }
}