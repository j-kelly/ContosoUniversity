namespace ContosoUniversity.Web.App.Tests
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;

    public class Host
    {
        public Host()
        {
            var options = new ChromeOptions();
            options.AddArguments("test-type");

            var service = ChromeDriverService.CreateDefaultService(@"..\..\WebDriver");
            service.HideCommandPromptWindow = false;
            WebDriver = new ChromeDriver(service, options);
            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

            Page = new Page(WebDriver);
        }

        public IWebDriver WebDriver { get; }

        public Page Page { get; }
    }
}