namespace System
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class SystemDateTime
    {
        private static Func<DateTime> today = () => DateTime.Today;
        private static Func<DateTime> now = () => DateTime.Now;

        public static DateTime Today
        {
            get { return today().Date; }
        }

        public static DateTime Now
        {
            get { return now(); }
        }

        public static void SetToday(DateTime dateToday)
        {
            today = () => dateToday;
        }

        public static void SetNow(DateTime dateNow)
        {
            now = () => dateNow;
        }

        public static void SetAll(DateTime current)
        {
            today = () => current;
            now = () => current;
        }
    }
}