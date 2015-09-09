Feature: Course_

# ######################################################################################
#
# Check courses creation
#
# ######################################################################################
Scenario: Check the creation of a new course
	Given I'm at at the "Course" page
	And I have the following departments
		| Name          | Budget  | StartDate   |
		| MyDepartment  | 123.1   | 01-Oct-2010 |
	When I select the "Create New" link
	And I enter the following details
		| Key          | Value        |
		| CourseID     | 1234         |
		| Title        | Course Title |
		| Credits      | 1            |
		| DepartmentID | MyDepartment |
	And I press the "Create" button 
	Then I expect the following info displayed in the "courses" table
		| Row | Column1 | Column2      | Column3 | Column4      | Column5                 |
		| 1   | 1234    | Course Title | 1       | MyDepartment | Edit ! Details ! Delete |


# ######################################################################################
#
# Check the edits of courses
#
# ######################################################################################
Scenario: Check course edit 
	Given  I have the following departments
		| Name              | Budget | StartDate   |
		| MyDepartment | 123.1  | 01-Oct-2010 |
		| AnotherDepartment | 923.1  | 01-Oct-2011 |
	And I have the following courses
		| CourseID | Title     | Credits | Department Name   |
		| 100      | My Course | 1       | MyDepartment |
	And I'm at at the "Course" page
	When I select the "Edit" link on the "courses" table on row "1" 
	And I enter the following details
		| PropertyName | Value             |
		| Title        | New Title         |
		| Credits      | 3                 |
		| DepartmentID | AnotherDepartment |
	And I press the "Save" button 
	Then I expect the following info displayed in the "courses" table
		| Row | Column1 | Column2   | Column3 | Column4           | Column5                 |
		| 1   | 100     | New Title | 3       | AnotherDepartment | Edit ! Details ! Delete |


# ######################################################################################
#
# Check the deletion of courses
#
# ######################################################################################
Scenario: Check course delete 
	Given  I have the following departments
		| Name              | Budget | StartDate   |
		| MyDepartment      | 123.1  | 01-Oct-2010 |
	And I have the following courses
		| CourseID | Title       | Credits | Department Name | 
		| 100      | My Course   | 1       | MyDepartment    |
	And I'm at at the "Course" page
	When I select the "Delete" link on the "courses" table on row "1" 
	And I select the "Back to List" link 
	And I select the "Delete" link on the "courses" table on row "1" 
	And I press the "Delete" button
	Then I expect the "courses" table to be empty
		