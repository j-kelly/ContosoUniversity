namespace ContosoUniversity.Web.Automation.Tests.Scaffolding.Data
{
    using Core.Repository;
    using Domain.AppServices;
    using Domain.Core.Behaviours.CourseApplicationService;
    using Domain.Core.Behaviours.DepartmentApplicationService;
    using Domain.Core.Behaviours.InstructorApplicationService;
    using Domain.Core.Behaviours.StudentApplicationService;
    using Domain.Core.Repository.Entities;
    using Domain.Services.DepartmentApplicationService;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using TechTalk.SpecFlow;

    [Binding]
    public static class DataHelper
    {
        private static List<AddedEntityInfo> _AddedEntities = null;

        [BeforeScenario]
        public static void BeforeScenario()
        {
            _AddedEntities = new List<AddedEntityInfo>();
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            using (var repository = new ContosoUniversityEntityFrameworkRepository())
            {
                foreach (var entity in _AddedEntities)
                {
                    switch (entity.Type)
                    {
                        case (EntityType.Department):
                            var commandModel = new DeleteDepartment.CommandModel { DepartmentID = (int)entity.Id };
                            new DepartmentApplicationService(repository).DeleteDepartment(new DeleteDepartment.Request("test", commandModel));
                            break;
                        case (EntityType.Student):
                            var commandModel1 = new DeleteStudent.CommandModel { StudentId = (int)entity.Id };
                            new StudentApplicationService(repository).DeleteStudent(new DeleteStudent.Request("test", commandModel1));
                            break;
                        case (EntityType.Instructor):
                            var commandModel2 = new DeleteInstructor.CommandModel { InstructorId = (int)entity.Id };
                            new InstructorApplicationService(repository).DeleteInstructor(new DeleteInstructor.Request("test", commandModel2));
                            break;
                        case (EntityType.Course):
                            var commandModel3 = new DeleteCourse.CommandModel { CourseId = (int)entity.Id };
                            new CourseApplicationService(repository).DeleteCourse(new DeleteCourse.Request("test", commandModel3));
                            break;
                        default:
                            throw new ApplicationException($"Missing entity removal for type {entity.Type}");
                    }
                }
            }
        }

        public static void AddEntityToRemove(EntityType type, object id)
        {
            _AddedEntities.Add(new AddedEntityInfo(type, id));
        }

        public static T CreateCommandModelFromTable<T>(Table table, TableRow row) where T : class
        {
            var commandType = typeof(T);
            var commandModel = Activator.CreateInstance(typeof(T));

            var unhandledItems = new List<KeyValuePair<string, string>>();
            var extraColumnMappings = CreateCustomMappings();
            for (int i = 0; i < table.Header.Count(); i++)
            {
                var stringVal = row.ElementAt(i).Value;

                string columnHeaderName = table.Header.ElementAt(i);

                var columnMapping = extraColumnMappings.SingleOrDefault(p => p.ColumnHeaderName == columnHeaderName);
                if (columnMapping != null)
                {
                    var propertyInfo = commandType.GetProperty(columnMapping.CommandModelPropertyName);
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
                    //}
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
            {
                Debugger.Launch();
                throw new Exception("unused items found: " + string.Join(", ", unhandledItems.Select(p => p.Key)));
            }

            return (T)commandModel;
        }

        public static IEnumerable<ColumnHeaderMapping> CreateCustomMappings()
        {
            var retVal = new List<ColumnHeaderMapping>();

            retVal.Add(new ColumnHeaderMapping(
               "Department Name",
               "DepartmentID",
               deptName =>
               {
                   using (var repository = new ContosoUniversityEntityFrameworkRepository())
                   {
                       var department = repository.GetEntity<Department>(p => p.Name == deptName);
                       return department.DepartmentID;
                   }
               }));

            retVal.Add(new ColumnHeaderMapping(
               "Selected Courses",
               "SelectedCourses",
               courseNames =>
               {
                   using (var repository = new ContosoUniversityEntityFrameworkRepository())
                   {
                       var titles = courseNames.Split(',').Select(p => p.Trim());
                       var courses = repository.GetEntities<Course>(p => titles.Contains(p.Title));
                       return courses.Select(p => p.CourseID).ToArray();
                   }
               }));

            retVal.Add(new ColumnHeaderMapping(
               "Instructor Name",
               "InstructorID",
               instructorName =>
               {
                   var firstName = instructorName.Split(' ')[0];
                   var lastName = instructorName.Split(' ')[1];

                   using (var repository = new ContosoUniversityEntityFrameworkRepository())
                   {
                       var instructor = repository.GetEntity<Instructor>(p => p.FirstMidName == firstName && p.LastName == lastName);
                       return instructor.ID;
                   }
               }));

            return retVal;
        }
    }
}