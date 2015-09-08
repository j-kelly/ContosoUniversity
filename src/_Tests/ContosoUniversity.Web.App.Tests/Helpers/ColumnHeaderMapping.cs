namespace ContosoUniversity.Web.App.Tests.Helpers
{
    using System;

    public class ColumnHeaderMapping
    {
        public ColumnHeaderMapping(string columnHeaderName, string propertyName, Func<string, object> getValueFunc)
        {
            ColumnHeaderName = columnHeaderName;
            PropertyName = propertyName;
            GetValueFunc = getValueFunc;
        }

        public string ColumnHeaderName { get; set; }
        public string PropertyName { get; set; }
        public Func<string, object> GetValueFunc { get; set; }
    }
}
