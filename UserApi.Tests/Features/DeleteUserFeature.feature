Feature: DeleteUserFeature
	In order to delete users from the API
	As an API consumer
	I want to get delete users by ID

Scenario: Delete user by Id
	Given that a user exists in the system
	When I request to delete the user by Id
	Then the user should be removed from the repository
	And the response status code is '200 OK'

Scenario: Delete non-existing user by Id
	Given that a user does not exist in the system
	When I request to delete the user by Id
	Then the response status code is '404 Not Found'