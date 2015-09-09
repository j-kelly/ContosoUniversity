namespace ContosoUniversity.Web.Automation.Tests.Scaffolding
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using System.Diagnostics.CodeAnalysis;

    public class Page
    {
        [SuppressMessage("CodeRush", "Unused member")]
        public static string ServerUrl { get; } = $"http://localhost:{IisExpressHelper.Port}/";

        public Page(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebDriver WebDriver { get; }

        public void GotoUrl(string url)
        {
            url = $"{ServerUrl}{url}";
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClickOnLink(string linkText)
        {
            WebDriver.FindElement(By.LinkText(linkText)).Click();
        }

        public void InputInformation(string elementName, string text)
        {
            var element = WebDriver.FindElement(By.Name(elementName));
            switch (element.TagName)
            {
                case ("select"):
                    var selectElement = new SelectElement(element);
                    selectElement.SelectByText(text);
                    break;
                default:
                    try { element.Clear(); } catch { }
                    element.SendKeys(text);
                    break;
            }
        }

        public void PushButton(string buttonText)
        {
            var element = WebDriver.FindElement(By.XPath($"//input[@value = '{buttonText}']"));
            element.Click();
        }
    }
}
