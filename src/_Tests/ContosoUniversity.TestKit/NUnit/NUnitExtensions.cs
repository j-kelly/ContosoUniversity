namespace NUnit.Framework
{
    using System.Diagnostics;

    public static class NUnitExtensions
    {
        [DebuggerStepThrough]
        public static void ShouldEqual(this object actualValue, object expectedValue)
        {
            ShouldEqual(actualValue, expectedValue, string.Empty);
        }

        [DebuggerStepThrough]
        public static void ShouldEqual(this object actualValue, object expectedValue, string message)
        {
            if (expectedValue == null)
            {
                Assert.That(actualValue, Is.Null, message);
                return;
            }

            Assert.That(actualValue, Is.EqualTo(expectedValue), message);
        }

        [DebuggerStepThrough]
        public static void ShouldNotEqual(this object actualValue, object expectedValue)
        {
            ShouldNotEqual(actualValue, expectedValue, string.Empty);
        }

        [DebuggerStepThrough]
        public static void ShouldNotEqual(this object actualValue, object expectedValue, string message)
        {
            if (expectedValue == null)
            {
                Assert.That(actualValue, Is.Not.Null, message);
                return;
            }

            Assert.That(actualValue, Is.Not.EqualTo(expectedValue), message);
        }
    }
}