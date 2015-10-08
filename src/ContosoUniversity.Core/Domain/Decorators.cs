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
            var retVal = logger.LogTimings(0, () =>
            {
                return next.Compile().Invoke(command);
            });

            return retVal;
        }

        /// <summary>
        /// Alters the expression so any dependencies are created seperatelywhich  allows them to be disposed
        /// </summary>
        public static IDomainResponse AutoDispose<T>(Expression<Func<T, IDomainResponse>> next) where T : class, IDomainRequest
        {
            var arguments = default(IEnumerable<Expression>);
            var Invoke = default(Func<IEnumerable<object>, IDomainResponse>);
            switch (next.Body.NodeType)
            {
                case (ExpressionType.Call):
                    var callExpression = (MethodCallExpression)next.Body;
                    arguments = callExpression.Arguments;
                    Invoke = p => (IDomainResponse)callExpression.Method.Invoke(null, p.ToArray());
                    break;
                default:
                    throw new NotSupportedException($"{next.NodeType} is not supported");
            }

            var parameters = new List<object>();
            var bodyCallExpression = (MethodCallExpression)next.Body;
            try
            {
                foreach (var arg in bodyCallExpression.Arguments)
                {
                    if (arg.NodeType == ExpressionType.MemberAccess)
                    {
                        var x = (MemberExpression)arg;
                        var val = Expression.Lambda(x).Compile().DynamicInvoke();
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

                // Execute method
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