Feature: Course Feauteres
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Add a new course
	Given I'm at at the "Course" page
	And I have a department with the following values
	| Name         | Budget | StartDate   | InstructorID |
	| MyDepartment | 123.1  | 01-Oct-2010 | 1            |          
	When I select the "Create New" link
	And I enter the following details
	| Key        | Value        |
	| CourseID   | 1234         |
	| Title      | MyTitle      |
	| Credits    | 999.50       |
#	| Department | MyDepartment |
	And I press the "Create" button

		