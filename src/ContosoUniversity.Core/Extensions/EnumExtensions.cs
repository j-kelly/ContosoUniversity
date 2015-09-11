namespace System
{
    using System.Globalization;

    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string text) where T : struct, IConvertible
        {
            T result;

            if (!Enum.TryParse(text, true, out result))
            {
                return default(T);
            }

            if (!Enum.IsDefined(typeof(T), result))
            {
                return default(T);
            }

            return result;
        }

        public static T ToEnum<T>(this int value) where T : struct, IConvertible
        {
            if (!Enum.IsDefined(typeof(T), value))
            {
                return default(T);
            }

            return ToEnum<T>(value.ToString(CultureInfo.InvariantCulture));
        }

        // ***********************************************************************************************
        // If you are building this and get an error here it's because you are running this in VS2013 or below
        // Please use VS 2015 for this sample code
        public static T ToEnum<T>(this short value) where T : struct, IConvertible => ToEnum<T>((int)value);
    }
}