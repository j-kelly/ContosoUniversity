namespace ContosoUniversity.Web.Automation.Tests.Steps
{
    using OpenQA.Selenium;
    using Scaffolding;
    using TechTalk.SpecFlow;

    [Binding]
    public class ActionSteps
    {
        public Page Page { get; } = HostManager.Page;

        [Given(@"I'm at at the ""(.*)"" page")]
        [When(@"I'm at at the ""(.*)"" page")]
        [Then(@"I'm at at the ""(.*)"" page")]
        public void GivenIMAtAtThePage(string page)
        {
            Page.GotoUrl(page);
        }

        [When(@"I select the ""(.*)"" link")]
        [Then(@"I select the ""(.*)"" link")]
        public void WhenISelectTheLink(string linkText)
        {
            Page.ClickOnLink(linkText);
        }

        [When(@"I enter the following details")]
        [Then(@"I enter the following details")]
        public void WhenIEnterTheFollowingDetails(Table table)
        {
            foreach (var row in table.Rows)
                Page.InputInformation(row[0], row[1]);
        }

        [When(@"I press the ""(.*)"" button")]
        [Then(@"I press the ""(.*)"" button")]
        public void WhenIPressTheButton(string buttonText)
        {
            Page.PushButton(buttonText);
        }

        [When(@"I select the ""(.*)"" link on the ""(.*)"" table on row ""(.*)""")]
        [Then(@"I select the ""(.*)"" link on the ""(.*)"" table on row ""(.*)""")]
        public void WhenISelectTheLinkInTheTableOnRow(string linkText, string tableId, int row)
        {
            var element = Page.WebDriver.FindElement(By.Id(tableId));
            var tr = element.FindElements(By.XPath("//tr"))[row];

            var link = tr.FindElement(By.LinkText(linkText));
            link.Click();
        }
    }
}
