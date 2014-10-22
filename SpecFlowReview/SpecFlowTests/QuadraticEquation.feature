Feature: Negative Discriminant

@mytag
Scenario: a=1, b=0, c=2
	Given I have entered 1 into the a
	And I have entered 0 into b
	And I have entered 2 into c
	When I press solve
	Then the result should be error "Negative Discriminant"

