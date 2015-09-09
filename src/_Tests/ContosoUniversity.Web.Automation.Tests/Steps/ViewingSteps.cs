namespace ContosoUniversity.Web.Automation.Tests.Steps
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using Scaffolding;
    using TechTalk.SpecFlow;

    [Binding]
    public class ViewingSteps
    {
        public Page Page { get; } = HostManager.Page;

        [Then(@"I expect the following info displayed in the ""(.*)"" table")]
        public void ThenIExpectTheFollowingInfoDisplayedInTheTable(string tableId, Table table)
        {
            var element = Page.WebDriver.FindElement(By.Id(tableId));
            var trs = element.FindElements(By.XPath("//tr"));

            foreach (var row in table.Rows)
            {
                var tr = trs[int.Parse(row["Row"])];
                var tds = tr.FindElements(By.XPath("//td"));
                tds.Count.ShouldEqual(table.Header.Count - 1);

                int index = 1;
                foreach (var td in tds)
                {
                    var expectedText = row[index++].Replace("!", "|");
                    td.Text.ShouldEqual(expectedText);
                }
            }
        }

        [Then(@"I expect the ""(.*)"" table to be empty")]
        public void ThenIExpectTheTableToBeEmpty(string tableId)
        {
            var element = Page.WebDriver.FindElement(By.Id(tableId));
            var trs = element.FindElements(By.XPath("//tr"));
            trs.Count.ShouldEqual(1);
        }
    }
}
