namespace NUnit.Framework
{
    using System.Collections.Generic;
    using System.Linq;

    public static class NUnitEnumerableExtensions
    {
        public static TSource AtIndex<TSource>(this IEnumerable<TSource> source, int index)
        {
            return source.Skip(index).First();
        }

        public static TSource Second<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(1).First();
        }

        public static TSource Third<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(2).First();
        }

        public static TSource Fourth<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(3).First();
        }

        public static TSource Fifth<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(4).First();
        }

        public static TSource Sixeth<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(5).First();
        }

        public static TSource Seventh<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(6).First();
        }

        public static TSource Eighth<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(7).First();
        }

        public static TSource Nineth<TSource>(this IEnumerable<TSource> source)
        {
            return source.Skip(8).First();
        }
    }
}
