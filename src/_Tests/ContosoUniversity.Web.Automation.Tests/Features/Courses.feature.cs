﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ContosoUniversity.Web.Automation.Tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Course_")]
    public partial class Course_Feature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Courses.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Course_", "", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Check the creation of a new course")]
        public virtual void CheckTheCreationOfANewCourse()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Check the creation of a new course", ((string[])(null)));
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("I\'m at at the \"Course\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Budget",
                        "StartDate"});
            table1.AddRow(new string[] {
                        "MyDepartment",
                        "123.1",
                        "01-Oct-2010"});
#line 10
 testRunner.And("I have the following departments", ((string)(null)), table1, "And ");
#line 13
 testRunner.When("I select the \"Create New\" link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Key",
                        "Value"});
            table2.AddRow(new string[] {
                        "CourseID",
                        "1234"});
            table2.AddRow(new string[] {
                        "Title",
                        "Course Title"});
            table2.AddRow(new string[] {
                        "Credits",
                        "1"});
            table2.AddRow(new string[] {
                        "DepartmentID",
                        "MyDepartment"});
#line 14
 testRunner.And("I enter the following details", ((string)(null)), table2, "And ");
#line 20
 testRunner.And("I press the \"Create\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Row",
                        "Column1",
                        "Column2",
                        "Column3",
                        "Column4",
                        "Column5"});
            table3.AddRow(new string[] {
                        "1",
                        "1234",
                        "Course Title",
                        "1",
                        "MyDepartment",
                        "Edit ! Details ! Delete"});
#line 21
 testRunner.Then("I expect the following info displayed in the \"courses\" table", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Check course edit")]
        public virtual void CheckCourseEdit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Check course edit", ((string[])(null)));
#line 31
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Budget",
                        "StartDate"});
            table4.AddRow(new string[] {
                        "MyDepartment",
                        "123.1",
                        "01-Oct-2010"});
            table4.AddRow(new string[] {
                        "AnotherDepartment",
                        "923.1",
                        "01-Oct-2011"});
#line 32
 testRunner.Given("I have the following departments", ((string)(null)), table4, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                        "CourseID",
                        "Title",
                        "Credits",
                        "Department Name"});
            table5.AddRow(new string[] {
                        "100",
                        "My Course",
                        "1",
                        "MyDepartment"});
#line 36
 testRunner.And("I have the following courses", ((string)(null)), table5, "And ");
#line 39
 testRunner.And("I\'m at at the \"Course\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.When("I select the \"Edit\" link on the \"courses\" table on row \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                        "PropertyName",
                        "Value"});
            table6.AddRow(new string[] {
                        "Title",
                        "My Course"});
            table6.AddRow(new string[] {
                        "Credits",
                        "1"});
            table6.AddRow(new string[] {
                        "DepartmentID",
                        "MyDepartment"});
#line 41
 testRunner.Then("I expect to see the following values", ((string)(null)), table6, "Then ");
#line hidden
            TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                        "PropertyName",
                        "Value"});
            table7.AddRow(new string[] {
                        "Title",
                        "New Title"});
            table7.AddRow(new string[] {
                        "Credits",
                        "3"});
            table7.AddRow(new string[] {
                        "DepartmentID",
                        "AnotherDepartment"});
#line 46
 testRunner.And("I enter the following details", ((string)(null)), table7, "And ");
#line 51
 testRunner.And("I press the \"Save\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                        "Row",
                        "Column1",
                        "Column2",
                        "Column3",
                        "Column4",
                        "Column5"});
            table8.AddRow(new string[] {
                        "1",
                        "100",
                        "New Title",
                        "3",
                        "AnotherDepartment",
                        "Edit ! Details ! Delete"});
#line 52
 testRunner.Then("I expect the following info displayed in the \"courses\" table", ((string)(null)), table8, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Check course delete")]
        public virtual void CheckCourseDelete()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Check course delete", ((string[])(null)));
#line 62
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                        "Name",
                        "Budget",
                        "StartDate"});
            table9.AddRow(new string[] {
                        "MyDepartment",
                        "123.1",
                        "01-Oct-2010"});
#line 63
 testRunner.Given("I have the following departments", ((string)(null)), table9, "Given ");
#line hidden
            TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                        "CourseID",
                        "Title",
                        "Credits",
                        "Department Name"});
            table10.AddRow(new string[] {
                        "100",
                        "My Course",
                        "1",
                        "MyDepartment"});
#line 66
 testRunner.And("I have the following courses", ((string)(null)), table10, "And ");
#line 69
 testRunner.And("I\'m at at the \"Course\" page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.When("I select the \"Delete\" link on the \"courses\" table on row \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
 testRunner.And("I select the \"Back to List\" link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
 testRunner.And("I select the \"Delete\" link on the \"courses\" table on row \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
 testRunner.And("I press the \"Delete\" button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
 testRunner.Then("I expect the \"courses\" table to be empty", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
