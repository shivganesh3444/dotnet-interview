# Solution Documentation

**Candidate Name:** Ganesh Pawar  
**Completion Date:** 13/02/2026

I have Used Integrated GitHub Copilot with GPT 4.1 model. Asked proper prompts based on the requirments to get the things done.
I have used Ask and Agent mode for prompting. Used Ask mode to understand the current application and Agent mode to fix the existing issues.

E.g. 

Ask Mode Prompt:

Prompt1: Act as a senior software developer and review this entire application and understand it.
Prompt2: Can we implement JWT token based authentication an authorization for this api application?

Agent Mode Prompts:

Prompt1: Let's identify architectural and design problems in th entire application.
Prompt2: Let's refractor the application following the best practices and implement above recommendations.
Prompt3: Please proceed to add the repository layer
Prompt4: please proceed to implement the repository layer (ITodoRepository and TodoRepository) to handle data access.
Prompt5: Let's implement JWT token based authentication an authorization for this api application as suggsted by you.
Prompt6: Let's use persist users using current database approach.


---

## Problems Identified

_Describe the issues you found in the original implementation. Consider aspects like:_
- Architecture and design patterns
- Code quality and maintainability
- Security vulnerabilities
- Performance concerns
- Testing gaps

1. Tight Coupling Between Layers
•	Problem: The TodoController directly instantiates TodoService instead of using Dependency Injection (DI). This makes the code less testable and harder to maintain.
•	Solution: Use DI to inject TodoService into the controller. Register TodoService in the Program.cs file using builder.Services.AddScoped<TodoService>();.

2. Hardcoded Database Connection String
•	Problem: The SQLite connection string is hardcoded in TodoService and Program.cs. This is not secure and makes it difficult to change the database configuration.
•	Solution: Move the connection string to appsettings.json and retrieve it using IConfiguration in Program.cs and TodoService.

3. SQL Injection Vulnerability
•	Problem: SQL queries in TodoService are constructed using string interpolation, which makes the application vulnerable to SQL injection attacks.
•	Solution: Use parameterized queries with SqliteCommand to prevent SQL injection.

4. Lack of Error Handling in TodoService
•	Problem: The service methods do not handle exceptions, which could lead to unhandled exceptions and application crashes.
•	Solution: Add proper exception handling in TodoService methods and log errors using a logging framework.

5. Controller Design Issues
•	Problem: The controller methods are using HttpPost for operations like GetTodo, UpdateTodo, and DeleteTodo. These operations should use HTTP verbs that align with RESTful principles:
•	GET for retrieving data.
•	PUT for updates.
•	DELETE for deletions.
•	Solution: Update the HTTP methods to follow RESTful conventions.

6. Lack of Validation
•	Problem: There is no validation for the input models (Todo, GetTodoRequest, UpdateTodoRequest, DeleteTodoRequest).
•	Solution: Use data annotations (e.g., [Required], [StringLength]) in the models and validate them in the controller using ModelState.IsValid.

7. No Unit Tests for Error Scenarios
•	Problem: The unit tests do not cover error scenarios, such as invalid input, database errors, or non-existent resources.
•	Solution: Add tests for edge cases and error scenarios to ensure the application handles them gracefully.

8. No Separation of Concerns in TodoService
•	Problem: TodoService handles both business logic and data access, violating the Single Responsibility Principle.
•	Solution: Introduce a repository layer to handle data access and let TodoService focus on business logic.

9. No Logging in the Application
•	Problem: The application lacks logging, which makes it difficult to debug and monitor.
•	Solution: Use the built-in ASP.NET Core logging framework to log errors and important events.

10. No Unit Tests for the Controller
•	Problem: The controller tests are minimal and do not validate HTTP responses or error handling.
•	Solution: Use a mocking framework like Moq to mock TodoService and write comprehensive tests for the controller.

11. No Authentication or Authorization
•	Problem: The API is open to anyone, which is a security risk.
•	Solution: Implement authentication and authorization using ASP.NET Core Identity or JWT.

12. No Pagination for GetAllTodos
•	Problem: The GetAllTodos method retrieves all records, which could lead to performance issues with a large dataset.
•	Solution: Implement pagination to limit the number of records returned.

13. No API Documentation
•	Problem: The API lacks proper documentation for its endpoints.
•	Solution: Use Swagger (already added in Program.cs) to document the API endpoints.


## Architectural Decisions

_Explain the architecture you chose and why. Consider:_
- Design patterns applied
- Project structure changes
- Technology choices
- Separation of concerns

1. We have implemented repository pattern to separate data access logic from business logic. This allows for better maintainability and testability of the code.
2. We have used Dependency Injection (DI) to manage dependencies between classes, which promotes loose coupling and makes the code more testable.
3. We have moved the database connection string to appsettings.json for better security and configurability.
4. We have implemented proper error handling in the service layer to ensure that exceptions are caught and logged, preventing application crashes and improving debugging capabilities.
5. We have updated the controller to use appropriate HTTP verbs (GET, PUT, DELETE) for different operations, following RESTful principles.
6. We have added validation to the input models using data annotations and validated them in the controller to ensure that only valid data is processed.
7. We have added unit tests for both the service and controller layers, covering both successful operations and error scenarios to ensure the robustness of the application.
8. We have implemented logging using the built-in ASP.NET Core logging framework to log errors and important events, which helps in monitoring and debugging the application.
9. We have considered implementing authentication and authorization but deferred it for future improvements due to time constraints. This would be an important addition to secure the API in a production environment.
10. We have implemented pagination for the GetAllTodos endpoint to improve performance when dealing with large datasets. This allows clients to retrieve data in manageable chunks and reduces the load on the server.
11. We have used Swagger for API documentation, which provides an interactive interface for testing the API endpoints and helps developers understand how to use the API effectively.
12. We have structured the project to have clear separation of concerns, with controllers handling HTTP requests, services containing business logic, and repositories managing data access. This promotes a clean architecture and makes the codebase easier to maintain and extend in the future.
13. We have used single responsibility principle to ensure that each class has a clear
14. We have chosen to use SQLite as the database for simplicity and ease of setup, which is suitable for a small application like this. However, the architecture allows for easy switching to a different database if needed in the future.
15. We have used ASP.NET Core for building the API due to its performance, scalability, and built-in features like dependency injection, logging, and middleware support, which help in building a robust and maintainable application.

Important Implementation Details:

- Added Moq package to implement mockable object for unit tests project.
- Added database connection string in appsettings json file.
- Added Repository pattern to make application looslely coupled.
- Used IConfiguration interface, built in configuration service to access database connection string from appsettings file.
- Implemented parametarized queries to avoid sql injection.
- Implemented Interface based implmentation for ToDoService and used repository interface to call database methods inside ToDoService.
- Implemented built in logger service for logging mechanism.
- Implemented DI for Repository and TodoServie in program.cs. Also used connection string from appsettings json file in it.
- Added validation for Title and Description property in model class ToDo.
- Handled exception inside the ToDoService.
- Used constructor dependency injection inside the controller.
- Modifed the route to api/todos for better readability and understanding.
- Used appropriate Http verbs for the CRUD operations inside controller class to make sure it follows restful principles.
- Removed unnecessary class for used inside the controller to handle http request data inside the controller class.
- Used query parameter to the required places instead of from body based on the requirement.
- Added mockable objects and other required service and controller variables and assigned them inside the constructor.
- Removed unnecessary test cases and handled more Asserts statments to validate more responses.
- Implemented JWT Token Based Authentication and Authorization.
- Added all possible positive and negative test cases except for authentication and authorization.
- Added pagination to GetAllTodos method


## Trade-offs

_Discuss compromises you made and the reasoning behind them. Consider:_
- What did you prioritize?
- What did you defer or simplify?
- What alternatives did you consider?

What did you prioritize?

•	Security and Best Practices: Implemented parameterized queries to prevent SQL injection, JWT authentication for secure access, and validation for all input models.
•	Maintainability and Testability: Used the repository pattern, dependency injection, and clear separation of concerns (controllers, services, repositories, models).
•	Code Quality: Followed SOLID principles, added comprehensive unit tests (positive and negative cases), and ensured code readability and organization.
•	Developer Experience: Integrated Swagger for API documentation and used xUnit/Moq for testing.

What did you defer or simplify?

•	Password Hashing: Used SHA256 for password hashing, which is not ideal for production. Deferred implementation of stronger algorithms like bcrypt or PBKDF2.
•	Role-Based Authorization: Implemented basic [Authorize] but deferred fine-grained role or policy-based authorization.
•	Rate Limiting & Account Lockout: Did not implement rate limiting or account lockout for authentication endpoints.
•	CORS Policy: Did not configure a strict CORS policy; left as default.
•	Error Handling: Used basic error handling and logging, but did not implement global exception handling middleware or custom error responses.
•	Production Secrets Management: JWT secret is stored in appsettings.json for simplicity; deferred secure storage (e.g., environment variables, vault).
•	Advanced Pagination Metadata: Pagination returns only the current page of results.

What alternatives did you consider?

•	Authentication: Considered ASP.NET Core Identity for user management but chose JWT for simplicity and statelessness.
•	Database: Used SQLite for ease of setup and portability; considered SQL Server or PostgreSQL for more robust scenarios.
•	Testing Frameworks: Chose xUnit and Moq for their popularity and integration with .NET, but considered NUnit and other mocking libraries.
•	API Documentation: Used Swashbuckle/Swagger for out-of-the-box .NET support; considered NSwag as an alternative.



## How to Run

### Prerequisites
[List required software, versions, etc.]

Dotnet 8.0 SDK or later, SQLite, and a code editor like Visual Studio or VS Code.
We can use C# 12.0 features in this project as we are using .NET 8.0 SDK.


### Build
```bash
# Add your build commands
```
dotnet build

### Run
```bash
# Add your run commands
```
dotnet run --project TodoApi

### Test
```bash
# Add your test commands
```
dotnet test

---

## API Documentation

### Endpoints

#### Create TODO
```
Method: [POST]

URL: https://localhost:7186/api/todos

Request Body:

curl -X 'POST' \
  'https://localhost:7186/api/todos' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJHYW5lc2giLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImp0aSI6IjY5YzBlMmNhLTA0YzctNDdkNC1hMzM0LTg3NjI0M2UxN2FhNSIsImV4cCI6MTc3MTAwNjQ1NCwiaXNzIjoiVG9kb0FwaUlzc3VlciIsImF1ZCI6IlRvZG9BcGlBdWRpZW5jZSJ9.sE5o2dNzktm8jWi_Z3dikANQt7MDpjUMcGsQ8mIFdUI' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "title": "test",
  "description": "test",
  "isCompleted": true,
  "createdAt": "2026-02-13T17:14:36.494Z"
}'

Response: 

Code: 200

{
  "id": 6,
  "title": "test",
  "description": "test",
  "isCompleted": true,
  "createdAt": "2026-02-13T17:14:47.1488872Z"
}

```

#### Get TODO(s)
```
Method: [GET]

URL: https://localhost:7186/api/todos?pageNumber=1&pageSize=10

Request: 

curl -X 'GET' \
  'https://localhost:7186/api/todos?pageNumber=1&pageSize=10' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJHYW5lc2giLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImp0aSI6IjY5YzBlMmNhLTA0YzctNDdkNC1hMzM0LTg3NjI0M2UxN2FhNSIsImV4cCI6MTc3MTAwNjQ1NCwiaXNzIjoiVG9kb0FwaUlzc3VlciIsImF1ZCI6IlRvZG9BcGlBdWRpZW5jZSJ9.sE5o2dNzktm8jWi_Z3dikANQt7MDpjUMcGsQ8mIFdUI'

Response:	

Code: 200

[
  {
    "id": 6,
    "title": "test",
    "description": "test",
    "isCompleted": true,
    "createdAt": "2026-02-13T22:44:47.0909145+05:30"
  }
]

```

### Get By ID TODO

Method: [GET]

URL:

Request: https://localhost:7186/api/todos/6

curl -X 'GET' \
  'https://localhost:7186/api/todos/6' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJHYW5lc2giLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImp0aSI6IjY5YzBlMmNhLTA0YzctNDdkNC1hMzM0LTg3NjI0M2UxN2FhNSIsImV4cCI6MTc3MTAwNjQ1NCwiaXNzIjoiVG9kb0FwaUlzc3VlciIsImF1ZCI6IlRvZG9BcGlBdWRpZW5jZSJ9.sE5o2dNzktm8jWi_Z3dikANQt7MDpjUMcGsQ8mIFdUI'

Response:

Code: 200

{
  "id": 6,
  "title": "test",
  "description": "test",
  "isCompleted": true,
  "createdAt": "2026-02-13T22:44:47.0909145+05:30"
}

#### Update TODO
```
Method: [PUT]

URL: https://localhost:7186/api/todos/6

Request Body: 

curl -X 'PUT' \
  'https://localhost:7186/api/todos/6' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJHYW5lc2giLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImp0aSI6IjY5YzBlMmNhLTA0YzctNDdkNC1hMzM0LTg3NjI0M2UxN2FhNSIsImV4cCI6MTc3MTAwNjQ1NCwiaXNzIjoiVG9kb0FwaUlzc3VlciIsImF1ZCI6IlRvZG9BcGlBdWRpZW5jZSJ9.sE5o2dNzktm8jWi_Z3dikANQt7MDpjUMcGsQ8mIFdUI' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "title": "up",
  "description": "up",
  "isCompleted": true,
  "createdAt": "2026-02-13T17:11:50.652Z"
}'

Response: 

Code: 200

{
  "id": 6,
  "title": "up",
  "description": "up",
  "isCompleted": true,
  "createdAt": "2026-02-13T17:11:50.652Z"
}

```

#### Delete TODO
```
Method: [DELETE]

URL: https://localhost:7186/api/todos/6

Request: 

curl -X 'DELETE' \
  'https://localhost:7186/api/todos/6' \
  -H 'accept: */*' \
  -H 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJHYW5lc2giLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImp0aSI6IjY5YzBlMmNhLTA0YzctNDdkNC1hMzM0LTg3NjI0M2UxN2FhNSIsImV4cCI6MTc3MTAwNjQ1NCwiaXNzIjoiVG9kb0FwaUlzc3VlciIsImF1ZCI6IlRvZG9BcGlBdWRpZW5jZSJ9.sE5o2dNzktm8jWi_Z3dikANQt7MDpjUMcGsQ8mIFdUI'

Response: 

Code: 200

{
  "message": "Todo deleted successfully"
}

```

---

## Future Improvements

_What would you do if you had more time? Consider:_
- Additional features
- Performance optimizations
- Enhanced testing
- Better documentation
- Deployment considerations

1. JWT Secret in appsettings.json
•	Issue: The JWT signing key is stored in plain text in appsettings.json.
•	Risk: If the file is leaked or checked into source control, attackers can forge tokens.
•	Mitigation: Store secrets in environment variables or a secure vault (e.g., Azure Key Vault, AWS Secrets Manager) in production.
2. Password Storage
•	Issue: Passwords are hashed with SHA256, but not salted.
•	Risk: SHA256 without a salt is vulnerable to rainbow table attacks.
•	Mitigation: Use a strong password hashing algorithm like PBKDF2, bcrypt, or Argon2, which includes salting and multiple iterations.
3. SQL Injection
•	Status: The code uses parameterized queries with AddWithValue, which mitigates SQL injection risks. This is good practice.
4. Error Handling
•	Issue: Some error messages may expose internal details (e.g., exception messages returned in BadRequest).
•	Risk: Revealing stack traces or internal errors can help attackers.
•	Mitigation: Return generic error messages to clients and log detailed errors server-side.
5. User Enumeration
•	Issue: The registration and login endpoints may reveal whether a username exists.
•	Risk: Attackers can enumerate valid usernames.
•	Mitigation: Return generic error messages for authentication failures.
6. Lack of HTTPS Enforcement
•	Issue: The code does not enforce HTTPS in production.
•	Risk: JWT tokens and credentials could be intercepted over HTTP.
•	Mitigation: Enforce HTTPS in production environments.
7. No Account Lockout or Rate Limiting
•	Issue: No rate limiting or account lockout on login attempts.
•	Risk: Vulnerable to brute-force attacks.
•	Mitigation: Implement rate limiting and account lockout after repeated failed attempts.
8. No Role-Based Authorization
•	Issue: [Authorize] is used, but there is no fine-grained role or policy-based authorization.
•	Risk: All authenticated users have the same access.
•	Mitigation: Use roles or policies for sensitive endpoints.
9. No CORS Policy
•	Issue: No CORS policy is defined.
•	Risk: May allow unwanted cross-origin requests.
•	Mitigation: Define a strict CORS policy.
10. No Refresh Token Mechanism
•	Issue: The implementation does not include a refresh token mechanism for JWT.
•	Risk: Users will have to log in again after the access token expires, which can be inconvenient.
•	Mitigation: Implement a refresh token mechanism to allow users to obtain new access tokens without re-authenticating. This can improve user experience while maintaining security.
11. Deployment Considerations
•	Issue: The application is not configured for deployment (e.g., no Dockerfile, no CI/CD pipeline).
•	Risk: Deployment may be error-prone and inconsistent across environments.
•	Mitigation: Create a Dockerfile for containerization and set up a CI/CD pipeline (e.g., GitHub Actions, Azure DevOps) to automate testing and deployment processes. This will ensure consistent deployments and reduce manual errors.
