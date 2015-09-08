namespace ContosoUniversity.Web.App.Tests
{
    using OpenQA.Selenium;
    using System;

    public class Page
    {
        public Page(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        public IWebDriver WebDriver { get; }

        public void GotoUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public void ClickOnLink(string linkText)
        {
            WebDriver.FindElement(By.LinkText(linkText)).Click();
        }

        public void InputInformation(string elementName, string text)
        {
            var element = WebDriver.FindElement(By.Name(elementName));
            //if(element is ComboBox)
            element.SendKeys(text);
        }

        public void EnterText(string elementName, string text)
        {
            var element = WebDriver.FindElement(By.Name(elementName));
            element.SendKeys(text);
        }
    }
}
