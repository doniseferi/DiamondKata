Feature: Diamond

Scenario: A diamond is printed correctly onto the console
	Given I want to print a diamond onto console
	When I provide a valid english character to the console
	Then a correctly formatted diamond is printed

Scenario: The application exits with a non-zero code on invalid input
	Given an invalid english character is specified:
		| Invalid Char |
		| @            |
		| 1            |
		| ẞ            |
		| Ë            |
	When The input is passed into the application
	Then the application should exit with a non-zero System Error code
	And the user is presented with a human readable message