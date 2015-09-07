namespace NUnit.Framework
{
    using Moq;
    using System;

    public static class Take
    {
        public static int AnyInt
        {
            get { return It.IsAny<int>(); }
        }

        public static int? AnyNullableInt
        {
            get { return It.IsAny<int?>(); }
        }

        public static DateTime AnyDateTime
        {
            get { return It.IsAny<DateTime>(); }
        }

        public static DateTime? AnyNullableDateTime
        {
            get { return It.IsAny<DateTime?>(); }
        }

        public static short AnyShort
        {
            get { return It.IsAny<short>(); }
        }

        public static short? AnyNullableShort
        {
            get { return It.IsAny<short?>(); }
        }

        public static string AnyString
        {
            get { return It.IsAny<string>(); }
        }

        public static bool AnyBool
        {
            get { return It.IsAny<bool>(); }
        }

        public static ushort AnyUShort
        {
            get { return It.IsAny<ushort>(); }
        }

        public static T Any<T>()
        {
            return It.IsAny<T>();
        }
    }
}
