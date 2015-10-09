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
            SystemDateTime.SetAll(DateTime.Now);
        }
    }
}
