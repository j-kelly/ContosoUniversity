Feature: Students_

# ######################################################################################
#
# Check the creation of new students
#
# ######################################################################################
Scenario: Check the creation of a new student
	Given I'm at at the "Student" page
	When I select the "Create New" link
	And I enter the following details
		| Key            | Value      |
		| LastName       | Kelly      |
		| FirstMidName   | John       |
		| EnrollmentDate | 10/10/1991 |
	And I press the "Create" button 
	Then I expect the following info displayed in the "students" table
		| Row | Column1 | Column2 | Column3    | Column5                 |
		| 1   | Kelly   | John    | 1991-10-10 | Edit ! Details ! Delete |


# ######################################################################################
#
# Check the edits of students
#
# ######################################################################################
Scenario: Check student edit 
	Given I have the following students
		| LastName | FirstMidName | EnrollmentDate |
		| Jones    | Jimbo        | 01-Oct-2010    |
	And I'm at at the "Student" page
	When I select the "Edit" link on the "students" table on row "1" 
	And I enter the following details
		| PropertyName   | Value       |
		| LastName       | Smith       |
		| FirstMidName   | John        |
		| EnrollmentDate | 01-03-2015 |
	And I press the "Save" button 
	Then I expect the following info displayed in the "students" table
		| Row | Column1 | Column2 | Column3    | Column5                 |
		| 1   | Smith   | John    | 2015-03-01 | Edit ! Details ! Delete |


# ######################################################################################
#
# Check the deletion of students
#
# ######################################################################################
Scenario: Check student delete 
	Given I have the following students
		| LastName | FirstMidName | EnrollmentDate |
		| Jones    | Jimbo        | 01-Oct-2010    |
	And I'm at at the "Student" page
	When I select the "Delete" link on the "students" table on row "1" 
	And I select the "Back to List" link 
	And I select the "Delete" link on the "students" table on row "1" 
	And I press the "Delete" button
	Then I expect the "students" table to be empty
