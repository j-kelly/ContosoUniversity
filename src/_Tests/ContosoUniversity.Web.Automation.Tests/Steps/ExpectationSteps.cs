namespace ContosoUniversity.Web.Automation.Tests.Steps
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using Scaffolding;
    using TechTalk.SpecFlow;

    [Binding]
    public class ExpectationSteps
    {
        public Page Page { get; } = HostManager.Page;

        [Then(@"I expect the following info displayed in the ""(.*)"" table")]
        [When(@"I expect the following info displayed in the ""(.*)"" table")]
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
        [When(@"I expect the ""(.*)"" table to be empty")]
        public void ThenIExpectTheTableToBeEmpty(string tableId)
        {
            var element = Page.WebDriver.FindElement(By.Id(tableId));
            var trs = element.FindElements(By.XPath("//tr"));
            trs.Count.ShouldEqual(1);
        }

        [Then(@"I expect to see the following values")]
        [When(@"I expect to see the following values")]
        public void ThenIExpectToSeeTheFollowingValues(Table table)
        {
            // PropertyName | Value | IsEditable
            foreach (var row in table.Rows)
            {
                var element = Page.WebDriver.FindElement(By.Id(row[0]));
                if (element.TagName == "select")
                {
                    var selectElement = new SelectElement(element);
                    selectElement.SelectedOption.Text.ShouldEqual(row[1]);
                    return;
                }

                element.GetAttribute("value").ShouldEqual(row[1]);
            }
        }

        [When(@"I expect the ""(.*)"" table to contain ""(.*)"" rows")]
        [Then(@"I expect the ""(.*)"" table to contain ""(.*)"" rows")]
        public void ThenIExpectTheTableToContainRows(string tableId, int expectedRows)
        {
            var element = Page.WebDriver.FindElement(By.Id(tableId));
            var acutualRowCount = element.FindElements(By.XPath("//tr")).Count - 1;
            acutualRowCount.ShouldEqual(expectedRows);
        }
    }
}
