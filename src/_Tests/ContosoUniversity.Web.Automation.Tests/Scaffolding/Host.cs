namespace ContosoUniversity.Web.Automation.Tests.Scaffolding
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System;

    public class Host
    {
        public Host()
        {
            // Hack 
            int retryCount = 3;
            while (true)
            {
                try
                {
                    var options = new ChromeOptions();
                    options.AddArguments("test-type");

                    var service = ChromeDriverService.CreateDefaultService(@"..\..\Scaffolding\WebDriver");
                    service.HideCommandPromptWindow = false;
                    WebDriver = new ChromeDriver(service, options);
                    WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));

                    Page = new Page(WebDriver);
                    Page.GotoUrl("Home");
                    break;
                }
                catch
                {
                    if (retryCount-- == 0)
                        throw;
                }
            }

        }

        public IWebDriver WebDriver
        {
            get;
        }

        public Page Page
        {
            get;
        }
    }
}