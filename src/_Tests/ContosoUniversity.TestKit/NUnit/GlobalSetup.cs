namespace ContosoUniversity.TestKit
{
    using NUnit.Framework;
    using System;

    [SetUpFixture]
    public class GlobalSetup
    {
        [SetUp]
        public void ShowSomeTrace()
        {
            DateTimeHelper.SetAll(DateTime.Now);
        }
    }
}
