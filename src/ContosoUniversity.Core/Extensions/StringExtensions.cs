namespace System
{
    using System.Diagnostics;

    public static class StringExtensions
    {
        [DebuggerStepThrough]
        public static string OfMaxLength(this string str, int maxLength)
        {
            if (str == null) 
                return null; 

            if (maxLength < 1)
                throw new ArgumentException("maxLength must be greater than 0");

            if (str.Length > maxLength)
                str = str.Substring(0, maxLength);

            return str;
        }

        [DebuggerStepThrough]
        public static string SafeTrim(this string str, string nullReplacement = null)
        {
            if (str == null)
                return nullReplacement;

            return str.Trim();
        }
    }
}
