namespace ContosoUniversity.Core.Utilities
{
    using System.Diagnostics;

    internal class DebugCheck
    {
        [Conditional("DEBUG")]
        public static void NotNull<T>(T value) where T : class
        {
            Debug.Assert(value != null);
        }

        [Conditional("DEBUG")]
        public static void NotNull<T>(T? value) where T : struct
        {
            Debug.Assert(value != null);
        }

        [Conditional("DEBUG")]
        public static void NotNullEmptyOrEmpty(string value)
        {
            Debug.Assert(!string.IsNullOrEmpty(value));
        }

        [Conditional("DEBUG")]
        public static void NotNullEmptyOrWhiteSpace(string value)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(value));
        }
    }
}
