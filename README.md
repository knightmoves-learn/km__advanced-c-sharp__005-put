# 005 Put

## Lecture

[![005 Put](https://img.youtube.com/vi/FClloXhn6b8/0.jpg)](https://www.youtube.com/watch?v=FClloXhn6b8) 

## Instructions

In this assignment you will continue working on our HomeEnergyApi's Controller. Similarly to the lecture, you will add a PUT request.

In `HomeEnergyApi/Controllers/HomesController.cs`...

- Create a new HTTP PUT method
  - This method should take one route parameter, off of the initial route `/Homes`.
  - This method should update a specific `Home` from the list `homesList`.
  - The Home being updated, should be the `Home` in `homesList` whose `ownerLastName` property is the same as the route parameter being passed in to your new PUT method.
  - This method should return the `Home` being updated.
  - If no `ownerLastName`s in `homesList` match the passed route paramter, this method should return `null`.
    - This will throw a warning, this is OK for now. It should not stop your API from running.
- Verify HomeEnergyApi can...
  - Get homes existing in the static list `homesList` from its GET method.
  - Add homes to the static list `homesList` from its POST method.
  - Update homes in the static list `homesList` from its PUT method.
  - Have any and all changes made to homes in `homesList` reflected as long as server is running. 
- Any existing methods or properties on `HomesController.cs` should NOT be changed.
- All methods should use the base route `/Homes`.

Additional Information:

- The `Home` type is defined for you as a record in `HomeEnergyApi/Models/HomeModel.cs`, do not change this definition.
- You should ONLY make code changes in `HomeEnergyApi/Controllers/HomesController.cs` to complete this assignment.

## Building toward CSTA Standards:

- Explain how abstractions hide the underlying implementation details of computing systems embedded in everyday objects (3A-CS-01) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs (3B-AP-16) https://www.csteachers.org/page/standards

## Resources

- https://en.wikipedia.org/wiki/REST
- https://en.wikipedia.org/wiki/HTTP
- https://en.wikipedia.org/wiki/HTTP#Request_methods

Copyright &copy; 2025 Knight Moves. All Rights Reserved.
