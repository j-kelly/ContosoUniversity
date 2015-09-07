namespace ContosoUniversity.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Check
    {
        public static T NotNull<T>(T value, string parameterName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static T? NotNull<T>(T? value, string parameterName) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        public static string NotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var msg = string.Format("The argument '{0}' cannot be null or empty.", parameterName);
                throw new ArgumentException(msg, parameterName);
            }

            return value;
        }

        public static string NotNullOrWhiteSpace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                var msg = string.Format("The argument '{0}' cannot be null, empty or contain only white space.", parameterName);
                throw new ArgumentException(msg, parameterName);
            }

            return value;
        }

        public static IEnumerable<T> NotEmpty<T>(IEnumerable<T> list, string parameterName)
        {
            if (!list.Any())
            {
                var msg = string.Format("The argument '{0}' cannot be an empty list.", parameterName);
                throw new ArgumentException(msg, parameterName);
            }

            return list;
        }
    }

}
