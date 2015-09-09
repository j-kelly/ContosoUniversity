namespace ContosoUniversity.Web.Automation.Tests.Scaffolding
{
    using System;

    public class ColumnHeaderMapping
    {
        public ColumnHeaderMapping(string columnHeaderName, string commandModelPropertyName, Func<string, object> getValueFunc)
        {
            ColumnHeaderName = columnHeaderName;
            CommandModelPropertyName = commandModelPropertyName;
            GetValueFunc = getValueFunc;
        }

        public string ColumnHeaderName { get; set; }
        public string CommandModelPropertyName { get; set; }
        public Func<string, object> GetValueFunc { get; set; }
    }
}
