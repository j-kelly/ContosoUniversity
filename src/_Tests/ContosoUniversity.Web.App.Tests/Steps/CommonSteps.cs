namespace ContosoUniversity.Web.App.Tests.Steps
{
    using OpenQA.Selenium;
    using TechTalk.SpecFlow;

    [Binding]
    public class CommonSteps
    {
        public Page Page { get; } = HostManager.Page;

        [Given(@"I'm at at the ""(.*)"" page")]
        public void GivenIMAtAtThePage(string page)
        {
            Page.GotoUrl($"{HostManager.ServerUrl}{page}");
        }

        [When(@"I select the ""(.*)"" link")]
        public void WhenISelectTheLink(string linkText)
        {
            HostManager.Page.ClickOnLink(linkText);
        }

        [When(@"I enter the following details")]
        public void WhenIEnterTheFollowingDetails(Table table)
        {
            foreach (var row in table.Rows)
            {
                HostManager.Page.InputInformation(row[0], row[1]);
            }
        }

        [When(@"I press the ""(.*)"" button")]
        public void WhenIPressTheButton(string buttonText)
        {
            var xPath = $"//input[@value = '{buttonText}']";
            var element = HostManager.Page.WebDriver.FindElement(By.XPath(xPath));
            element.Click();
        }
    }
}
