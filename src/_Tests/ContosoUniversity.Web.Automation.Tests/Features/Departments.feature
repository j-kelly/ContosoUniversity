Feature: Departments

Scenario: Create, modify and delete test

# setup
	Given I have the following instructors
		| LastName | FirstMidName | HireDate    | OfficeLocation |
		| Lemmon   | Keith        | 01-Oct-2010 | Bath           |
		| Marley   | Bob          | 01-Oct-2010 | Bath           |
	And I'm at at the "Department" page

# Create
	When I select the "Create New" link
	And I select the "Back to List" link 
	And I expect the "departments" table to be empty
	When I select the "Create New" link
	And I enter the following details
		| Key          | Value         |
		| Name         | MyDept        |
		| Budget       | 9.99          |
		| StartDate    | 01-01-2001    |
		| InstructorID | Lemmon, Keith |
	And I press the "Create" button 
	Then I expect the following info displayed in the "departments" table
		| Row | Column1 | Column2 | Column3             | Column4 | Column5                 |
		| 1   | MyDept  | 9.99    | 01/01/2001 00:00:00 | Lemmon  | Edit ! Details ! Delete |

# Details 
	And I select the "Details" link on the "departments" table on row "1"
	And I select the "Back to List" link 
	And I select the "Details" link on the "departments" table on row "1"
	And I select the "Edit" link 
	And I select the "Back to List" link 

# Edit
	And I select the "Edit" link on the "departments" table on row "1"
	And I select the "Back to List" link 
	And I select the "Edit" link on the "departments" table on row "1"
	And I enter the following details
		| Key          | Value       |
		| Name         | MyDept2     |
		| Budget       | 99.99       |
		| StartDate    | 01-01-2009  |
		| InstructorID | Marley, Bob |
	And I press the "Save" button 
	Then I expect the following info displayed in the "departments" table
		| Row | Column1 | Column2 | Column3             |  Column4 | Column5                 |
		| 1   | MyDept2 | 99.99   | 01/01/2009 00:00:00 |  Marley  | Edit ! Details ! Delete |

# delete
	And I select the "Delete" link on the "departments" table on row "1" 
	And I select the "Back to List" link 
	And I select the "Delete" link on the "departments" table on row "1" 
	And I press the "Delete" button
	And I expect the "departments" table to be empty



