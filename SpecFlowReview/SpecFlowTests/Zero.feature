Feature: Zero

@mytag
Scenario: a=1, b=2, c=1 (Discriminant = 0)
	Given I have entered 1 into the a
	And I have entered 2 into b
	And I have entered 1 into c
	When I press solve
	Then the result should be error "x=-1"
