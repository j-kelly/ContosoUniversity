namespace System
{
    using System.Diagnostics.CodeAnalysis;
    using Threading;

    [ExcludeFromCodeCoverage]
    public static class CurrentPrincipalHelper
    {
        private static readonly Func<string> defaultUserFunction = () =>
        {
            return string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name)
                ? "anon"
                : Thread.CurrentPrincipal.Identity.Name;
        };

        // Don't shift the order of this method
        private static Func<string> userIdFunction = defaultUserFunction;

        public static string Name
        {
            get { return userIdFunction.Invoke(); }
        }

        public static void SetName(string name)
        {
            userIdFunction = () => name;
        }

        public static Func<string> SetName(Func<string> nameFunc)
        {
            var retVal = userIdFunction;
            userIdFunction = nameFunc;
            return retVal;
        }

        public static void ResetName()
        {
            userIdFunction = defaultUserFunction;
        }
    }
}
