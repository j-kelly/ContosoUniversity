Feature: DataSetup

# ######################################################################################
#
# Test Data Setup
#
# ######################################################################################
Scenario: Example steps for setting up test data in the database

	# How to add departments
	Given  I have the following departments
		| Name            | Budget  | StartDate   |
		| MyDepartment    | 123.1   | 01-Oct-2010 |
		| XDepartment     | 12.1    | 01-Oct-2020 |
		| BobDepartment   | 23.1    | 01-Oct-2030 |
		| JKDepartment    | 13323.1 | 01-Oct-2040 |
		| Mums Department | 123.1   | 01-Oct-2040 |

	# how to add students
	And I have the following students
		| LastName | FirstMidName | EnrollmentDate |
		| Smith    | Tod          | 01-Oct-2010    |
		| Tod      | Sweeney      | 01-Oct-2010    |
		| Jones    | Jimbo        | 01-Oct-2010    |

	# how to add courses
	And I have the following courses
		| CourseID | Title       | Credits | Department Name | 
		| 100      | My Course   | 1       | MyDepartment    |
		| 101      | Your Course | 1       | MyDepartment    |
		| 102      | Jims Course | 1       | XDepartment     |
		| 103      | Bobs Course | 1       | BobDepartment   |

	# how to add instructors
	And I have the following instructors
		| LastName | FirstMidName | HireDate    | OfficeLocation | Selected Courses                    |
		| Lemmon   | Keith        | 01-Oct-2010 | Bath           | Bobs Course ,Jims Course            |
		| Cox      | John         | 01-Oct-2010 | Bath           | Bobs Course ,Jims Course, My Course |
		| Burges   | Arthur       | 01-Oct-2010 | London         | Bobs Course ,Jims Course            |
		| Osborne  | Fred         | 01-Oct-2010 | Brighton       | Your Course                         |

	When I'm at at the "Course" page
	Then I expect the "courses" table to contain "4" rows

	And I'm at at the "Student" page
	And I expect the "students" table to contain "3" rows

	When I'm at at the "Instructor" page
	Then I expect the "instructors" table to contain "4" rows
	
	When I'm at at the "Department" page
	Then I expect the "departments" table to contain "5" rows




# ######################################################################################
#
# All available steps (minus above steps)
#
# ######################################################################################
#Scenario: Example steps
#
#	#  Navigation
#	Given I'm at at the "Instructor" page
#
#	# clicks
#	When I select the "Create New" link
#	And I press the "Create" button 
#	And I select the "Edit" link on the "students" table on row "1" 
#
#	# Data entry
#	And I enter the following details
#	| PropertyId | Value      |
#	| Title      | Your title |
#	
#	# Assertions
#	Then I expect the "students" table to be empty 
#	And I expect the following info displayed in the "students" table 
#	| Row | Column1 | Column2      | Column3 | Column4     | Column5                 |
# | 1   | 1234    | Course Title | 1       | XDepartment | Edit ! Details ! Delete |

