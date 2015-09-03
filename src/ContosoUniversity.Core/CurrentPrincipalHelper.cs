namespace System
{
    using System.Diagnostics.CodeAnalysis;
    using Threading;

    [ExcludeFromCodeCoverage]
    public static class CurrentPrincipalHelper
    {
        private static readonly Func<string> httpCurrentUserFunction = () =>
        {
            return string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name)
                ? "anon"
                : Thread.CurrentPrincipal.Identity.Name;
        };

        // Don't shift the order of this method
        private static Func<string> userIdFunction = httpCurrentUserFunction;

        public static string UserId
        {
            get { return userIdFunction.Invoke(); }
        }

        public static void SetUserId(string name)
        {
            userIdFunction = () => name;
        }

        public static void ResetUserId()
        {
            userIdFunction = httpCurrentUserFunction;
        }
    }
}
