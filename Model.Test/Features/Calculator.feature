Feature: Calculator
	In calculator to get true result
	As a user
	I want to be check work of calculator

Scenario Outline: Correct expression
	
	Given I have standard processor with standart rules
	When I input expression <expression>
	Then the result is <result> within accuracy 1e-3

	 Examples: 
		| expression		| result |
		| 1+2-1				| 2.0    |
		| 2*2-1				| 3.0    |
		| 1+2*3/4			| 2.5    |
		| (2+4)*(3+6)		| 54     |
		| Max(2,4,8)		| 8.0    |
		| Min(3,4,Min(3,4)) | 3.0    |

Scenario Outline: Incorrect expression

	Given I have standard processor with standart rules
	When I input expression <expression>
	Then the result has errors

	 Examples: 
		| expression       |
		| 1+2-8*func(1))   |
		| -2+3/	           |
		| 16+41)-(2*3)     |
		| 1 / 45? + (3-6)  |
		| min(1,2)		   |

