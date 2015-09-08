namespace ContosoUniversity.Web.App.Tests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TechTalk.SpecFlow;

    public static class DataGeneration
    {
        public static T CreateCommandModelFromTable<T>(Table table, IEnumerable<ColumnHeaderMapping> extraColumnMappings = null) where T : class
        {
            var commandModel = Activator.CreateInstance(typeof(T));

            var unhandledItems = new List<KeyValuePair<string, string>>();
            var row = table.Rows.Single();
            var commandType = typeof(T);
            for (int i = 0; i < table.Header.Count(); i++)
            {
                var stringVal = row.ElementAt(i).Value;

                string columnHeaderName = table.Header.ElementAt(i);

                if (extraColumnMappings != null)
                {
                    var columnMapping = extraColumnMappings.SingleOrDefault(p => p.ColumnHeaderName == columnHeaderName);
                    if (columnMapping != null)
                    {
                        var propertyInfo = commandType.GetProperty(columnMapping.PropertyName);
                        if (propertyInfo != null)
                        {
                            var value = default(object);
                            try { value = columnMapping.GetValueFunc(stringVal); }
                            catch
                            {
                                unhandledItems.Add(row.ElementAt(i));
                                continue;
                            }

                            // Set the value of the property 
                            propertyInfo.SetValue(commandModel, value, null);
                            continue;
                        }
                    }
                }

                var pi = commandType.GetProperty(columnHeaderName);
                if (pi == null)
                {
                    unhandledItems.Add(row.ElementAt(i));
                    continue;
                }

                // Int
                if (pi.GetType() == typeof(int) || pi.PropertyType == typeof(int))
                {
                    var intVal = int.Parse(stringVal);
                    pi.SetValue(commandModel, intVal, null);
                    continue;
                }

                // Int
                if (pi.GetType() == typeof(int?) || pi.PropertyType == typeof(int?))
                {
                    var intVal = int.Parse(stringVal);
                    pi.SetValue(commandModel, intVal, null);
                    continue;
                }

                // Int
                if (pi.GetType() == typeof(decimal) || pi.PropertyType == typeof(decimal))
                {
                    var intVal = decimal.Parse(stringVal);
                    pi.SetValue(commandModel, intVal, null);
                    continue;
                }

                // DateTime
                if (pi.GetType() == typeof(DateTime) ||
                    pi.PropertyType == typeof(DateTime) ||
                    pi.GetType() == typeof(DateTime?) ||
                    pi.PropertyType == typeof(DateTime?))
                {
                    var dateValue = DateTime.Parse(stringVal);
                    pi.SetValue(commandModel, dateValue, null);
                    continue;
                }

                // its a string
                pi.SetValue(commandModel, stringVal, null);
            }

            if (unhandledItems.Any())
                throw new Exception("unused items found");

            return (T)commandModel;
        }
    }
}