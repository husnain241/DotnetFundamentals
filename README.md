# CMIntern4

### Project Overview

CMIntern4 is a .NET training project developed to practice and demonstrate core C# and ASP.NET Core development concepts.

The project progresses from C# fundamentals and Object-Oriented Programming to ASP.NET Core MVC, Repository Pattern, Unit of Work, Dependency Injection, Entity Framework Core, Bootstrap, and Unit Testing.

### Development Environment
- Visual Studio 2026
- .NET SDK
- Git
- GitLab
### Technologies Used
- C#
- .NET
- ASP.NET Core MVC
- Entity Framework Core
- SQL / In-Memory Database
- LINQ
- Bootstrap 5
- xUnit
- Moq
- Git & GitLab
- Stripe .NET SDK
### Key Features
- C# console applications
- Object-Oriented Programming
- Student Management System
- Vehicle Management System
- Employee Management System
- Payment processing with Stripe Test API
- LINQ-based searching, grouping, filtering, and aggregation
- ASP.NET Core MVC applications
- Student and Department CRUD operations
- Student image upload and management
- Bootstrap-based responsive UI
- Attribute Routing
- Dependency Injection
- Repository Pattern
- Unit of Work Pattern
- Entity Framework Core
- Service-based architecture
- Unit testing with xUnit and Moq
- Error handling and bug fixing

## Development Environment

- Visual Studio 2026 Installed
- .NET SDK Installed
- Git Installed
- GitLab Repository Cloned Successfully

## Completed Tasks

### Issue #1 - Development Environment Setup
- Development environment configured
- Repository cloned successfully
- README updated
- `.gitignore` added

### Issue #2 - First Console Application
- Created a Console Application
- Read two numbers from user input
- Performed arithmetic operations:
  - Addition
  - Subtraction
  - Multiplication
  - Division
- Input validation implemented using `TryParse`

### Issue #3 - Variables & Data Types
- Implemented variables and data types in the console application
- Used appropriate data types for user input and arithmetic operations
- Handled type conversions and casting where necessary
- Implemented error handling for invalid input and division by zero scenarios
Added comments and documentation for better code readability and maintainability

### Issue #4 - Control Statements
- Built a Number Guessing Game
- Implemented `if`, `else`, `while`, and `break`
- Generated a random number using the `Random` class
- Added input and range validation
- Tracked the number of attempts
- Implemented the Play Again (Y/N) feature
- Improved code readability using separate methods

### Issue #5 - Methods
-Created a Grade Calculator console application
- Used methods with parameters and return values
	- ReadMarks method to read user input
	- CalculateGrade method to calculate the grade based on marks
- Calculated grades based on marks
- Added the Play Again (Y/N) feature
- Improved code readability by separating responsibilities into methods

### Issue #6 - Arrays & Collections
- Created a Student Record System console application
- Used a List<Student> to store multiple student records
- Implemented CRUD operations:
  - Add Student
  - View Students
  - Search Student by ID
  - Update Student information
  - Delete Student by ID
- Used foreach loop to display and search student records
- Applied input validation for student information
- Organized the application using separate methods for each operation

### Issue #7 - Classes and Objects
- Created a Student class
- Implemented default and parameterized constructors
- Created multiple Student objects
- Demonstrated object creation using constructors and object initialization
- Displayed student information in the console
- Learned the concepts of classes, objects, constructors, and object initialization
- Understood reference types (Stack vs Heap), garbage collection, and the THIS keyword

### Issue #8 - Inheritance and Polymorphism
- Built a Vehicle Management System using inheritance and polymorphism
- Created a base `Vehicle` class with common properties and behaviors
- Implemented derived classes:
  - `Car`
  - `Truck`
  - `Motorcycle`
- Demonstrated inheritance by sharing common functionality through the base class
- Implemented method overriding using the `DisplayInfo()` method
- Applied runtime polymorphism by storing different vehicle types in a single `List<Vehicle>`
- Implemented complete CRUD operations:
  - Add Vehicle
  - View All Vehicles
  - Search Vehicle by ID
  - Update Vehicle
  - Delete Vehicle
- Updated both common vehicle properties and type-specific properties
- Implemented input validation using `TryParse()` and helper methods
- Used `FuelType` and `InputMode` enums for cleaner and more maintainable code
- Organized the project using a layered structure:
  - Models
  - Services
  - Helpers
  - UI
  - Data
  - Enums
- Applied professional coding practices:
  - Separation of Concerns (SoC)
  - Single Responsibility Principle (SRP)
  - Reusable helper methods
  - Clean and maintainable project structure


### Issue #9 - Encapsulation and Properties
- Created an Employee Management System
- Implemented encapsulation in the Employee class
  - Used private backing fields
  - Exposed data through public properties (`get` and `set`)
  - Added validation for Employee ID, Name, Department, and Monthly Salary
- Implemented calculated properties
  - Annual Salary
  - Salary Grade
- Created an EmployeeService class
  - Added employee records
  - Viewed all employees
  - Searched employees by Employee ID
  - Updated employee information
  - Deleted employee records.
	
### Issue #10 - Interfaces & Abstraction
- Designed and implemented a Payment System using interfaces and abstract classes.
- Created the IPaymentProcessor interface and PaymentProcessorBase abstract class.
- Implemented multiple payment gateways:
	- PayPal
	- Stripe
	- SecureNet
	- We can add more payment gateways in the future by implementing the IPaymentProcessor interface like Credit Card, Jazzcash, EasyPaisa etc.
- Applied runtime polymorphism to process payments through different payment processors.
- Integrated the Stripe .NET SDK with the Stripe Test API.
- Processed real test payments using Stripe test tokens (tok_visa).
- Implemented payment refund functionality using the Stripe API.
- Developed payment management features:
	- Create Payment
	- Process Payment
	- View Payment History
	- Refund Payment
- Applied clean architecture principles by separating UI, business logic, and payment gateway implementations.
- Improved code reusability and maintainability using helper classes and common validation logic

### Issue #12 - Advanced LINQ
- Implemented advanced LINQ operations for searching, grouping, counting, and data aggregation.
- Applied LINQ methods such as Any, All, Count, Sum, GroupBy, Distinct, Skip, Take, Contains, LastOrDefault, SingleOrDefault, and Join.
- Refactored the project by separating Student and Department models and establishing relationships using DepartmentId.
- Used LINQ Join to combine student and department data through a `StudentDepartmentDto`, `GetAverageMarksByDepartment`.
- Improved the project structure by updating the seeders, services, helpers, and program flow to support a clean, maintainable, and scalable design.

### Issue #13 - Git Workflow
- Created and switched to a feature branch.
- Made changes to the `README.md` file and committed the changes.
- Practiced the Git workflow by pushing changes to the remote repository and creating a Merge Request.
- Learned how to merge changes from one branch into another using `git merge`.
- Understood merge conflicts and practiced resolving them.
- Learned how to copy a specific commit from one branch to another using `git cherry-pick`.
- Practiced undoing committed changes safely using `git revert`.
- Practiced restoring uncommitted changes using `git restore`.
- Followed Git best practices, including meaningful commit messages, feature branch workflow, and keeping the local `main` branch up to date before starting new work.


### Issue #14 - Mini Project (Student Management System)
- Built a Student Management System console application using OOP, LINQ, and List<T>.
	- Implemented complete CRUD operations:
	- Add Student
	- View Students
	- Search Student by ID
	- Update Student
	- Delete Student
- Implemented additional LINQ features:
	- Search Student by Name
	- Show Top Students
	- Show Students by Department
	- Show Students Above Marks
	- Group Students by Department
	- Student Statistics (Highest, Lowest, Average, Department-wise Count)
- Separated the project into Models, Services, DTOs, Helpers, Constants, Enums, Interfaces, and Seeders.
- Applied object-oriented design principles and interface-based architecture using IStudentService and IDepartmentService.
- Used LINQ Join to combine Student and Department data through StudentDepartmentDto.
- Returned read-only collections using IReadOnlyList<T> where appropriate.
- Applied input validation, reusable helper methods, and clean project organization following professional coding practices.

#### -------------- Start ASP.NET Core Fundamentals ----------------- ###

### Issue #15 - Project Setup
- Created a new ASP.NET Core MVC solution and project.
- Configured the project using the .NET framework.
- Explored the basic ASP.NET Core MVC project structure.
- Built and ran the application successfully.
- Verified the default ASP.NET Core MVC application in the browser.
- Prepared a working project skeleton for upcoming ASP.NET Core Fundamentals issues.

### Issue #16 - Project Structure

- Organized the ASP.NET Core MVC project according to standard conventions.
- Created and organized the `Models` folder for application data models.
- Created and organized the `Controllers` folder for handling requests and application logic.
- Created and organized the `Views` folder for Razor views and UI pages.
- Created a basic Model, Controller, and View to understand their relationship.
- Verified the Controller-to-View flow using MVC conventions.
- Reviewed the purpose of the `wwwroot`, `Program.cs`, and `appsettings.json` files.
- Verified the final project structure and prepared it for upcoming ASP.NET Core MVC development.

### Issue #17 - Program.cs
- Reviewed and configured the Program.cs file as the application entry point.
- Configured the ASP.NET Core application builder and host.
- Registered MVC services using AddControllersWithViews().
- Configured the application middleware pipeline.
- Enabled HTTPS redirection and static file handling.
- Configured MVC controller routing with the default route.
- Started the application using app.Run().
- Built and ran the application successfully without errors.
- Verified that the ASP.NET Core MVC application starts and works correct

### Issue #18 - Configuration
- Configured application settings using appsettings.json and environment-specific settings.
- Practiced IConfiguration, environment variables, connection strings, API settings, and feature flags.
- Implemented strongly typed configuration using the Options Pattern.
- Configured User Secrets for sensitive development settings.
- Verified that configuration loads correctly without errors.

### Issue #19 - Middleware

- Implemented and explored the ASP.NET Core middleware pipeline.
- Practiced middleware execution order using `Use()`, `Run()`, `Map()`, and `MapWhen()`.
- Created custom middleware for request logging and request timing.
- Implemented exception handling middleware using `try-catch`.
- Practiced short-circuiting and conditional middleware branches.
- Verified request and response flow through the middleware pipeline.

### Issue #20 - Controllers
- Implemented and explored ASP.NET Core MVC controllers and controller actions.
- Practiced action methods with IActionResult, View(), RedirectToAction(), NotFound(), and BadRequest().
- Practiced model binding, Data Annotations, and ModelState validation.
- Explored data passing using Models, ViewBag, ViewData, and TempData.
- Implemented CRUD controller patterns using GET and POST actions.
- Practiced attribute routing and route parameters.
- Implemented Controller → Service architecture using Dependency Injection and interfaces.
- Explored controller context including HttpContext, Request, Response, User, RouteData, and ModelState.
- Implemented the Post/Redirect/Get (PRG) pattern using RedirectToAction().
- Practiced Action Filters and understood Middleware vs Filters.
- Explored exception handling using try-catch, Exception Filters, and Exception Middleware.
- Implemented centralized error handling and local exception recovery scenarios.

### Issue #21 - Views
- Strongly typed Razor Views using @model
- View discovery and MVC view conventions
- Model-to-View data passing
- Student list rendering with Razor
- Student details view
- Create Student form
- Edit Student form
- Form submission using Tag Helpers
- Model binding for Student properties
- Model validation and validation error display
- HTML Helpers such as DisplayFor and DisplayNameFor
- Tag Helpers such as asp-for, asp-action, asp-route-id, and asp-validation-for
- ViewBag, ViewData, and TempData usage
- Success messages using TempData after CRUD operations
- HTML encoding and basic XSS protection concepts
- Clean View practices by keeping business logic outside Views
- Bootstrap-based responsive UI for Student CRUD pages
- Create, Edit, Details, and Delete actions integrated with the existing StudentController

### Issue #22 — Razor Syntax
- Practiced Razor expressions using @.
- Created and used variables inside Razor code blocks.
- Used if, else, and nested conditions.
- Used foreach and for loops.
- Combined HTML with nested C# logic.
- Used Model, ViewBag, and ViewData with Razor.
- Implemented a practical Student listing using Razor syntax.
- Rendered student data dynamically with conditions and loops.

### Issue #23 — Layouts
- Created and configured `_Layout.cshtml` for shared page structure.
- Implemented shared Navbar, Footer, CSS, and JavaScript.
- Used `@RenderBody()` to render View-specific content.
- Used `@RenderSectionAsync()` for optional page-specific content.
- Practiced required and optional sections.
- Explored `_ViewStart.cshtml` for selecting default Layouts.
- Created `_AdminLayout.cshtml` for Admin pages.
- Implemented multiple Layouts for normal and Admin Views.
- Practiced Layout overriding at the View level.
- Learned nested Layouts.
- Used `ViewData` / `ViewBag` with Layouts.
- Implemented dynamic page titles using `ViewData["Title"]`.
- Learned common Layout mistakes and best practices.

### Issue #24 — Partial Views
- Created and used Partial Views for reusable UI components.
- Learned what Partial Views are and why they are used.
- Created reusable `_StudentList.cshtml`.
- Passed `IEnumerable<Student>` to the Partial View.
- Used the `<partial>` Tag Helper.
- Practiced passing single objects and collections.
- Reused the same Partial View with different data.
- Learned Partial Views with Layouts and nested Partial Views.
- Learned common mistakes and best practices.
- Used `PartialView()` from a Controller action.

### Issue #25 — Static Files
- Configured static file middleware using `UseStaticFiles()`.
- Created static CSS and JavaScript files inside `wwwroot`.
- Added and tested an image from `wwwroot/images`.
- Linked static files to Razor Views.
- Verified that static files load correctly in the browser.

### Issue #25 — Static Files

- Configured static file middleware using `UseStaticFiles()`.
- Added and served static files from the `wwwroot` folder.
- Implemented student image upload and display functionality.
- Added image validation, replacement, and deletion.

### Issue #26 — Mini Project: Student Management System

- Built a Student Management System using ASP.NET Core MVC fundamentals.
- Implemented Student CRUD operations with validation.
- Created a separate Department model and Department service.
- Connected students with departments using `DepartmentId`.
- Implemented a separate Image Service for student image management.
- Added department dropdowns in Create and Edit.
- Implemented department name display instead of showing `DepartmentId`.
- Currently working on student search/filter functionality.

### Issue # 48 - Repository Pattern
- Implemented the Repository Pattern for data access abstraction.
- Created generic repository interfaces and concrete implementations for Student entity.
- Implemented CRUD operations through the repository layer.
- Created the Service layer (IStudentService and StudentService).
- Connected Controller → Service → Repository.

### Issue # 49 - Unit of Work 
- Implemented Unit of Work Pattern: Integrated IUnitOfWork and UnitOfWork to centralize access to StudentRepository and DepartmentRepository.
- Completed Student–Department Relationship: Connected Student and Department models with Foreign Keys, navigation properties, and dynamic dropdown selections.
- Full CRUD Operations: Updated controllers, services, and views to handle complete CRUD flows for both Students and Departments cleanly.
- Understood End-to-End Data Flow: Mastered how request data flows through Controller → Service → Unit of Work → Repository → Database.
- Atomic Unit of Work Transactions: Learned how Unit of Work to coordinate multiple entity updates (e.g., updating Student department while syncing department StudentCount) so all changes succeed or fail together.
- Replaced In-Memory Lists with Real Local DB: Integrated Entity Framework Core (ApplicationDbContext) to practice real database context persistence and SaveChanges() execution.
- 
### Issue # 49 - Unit of Work 
- Implemented Unit of Work Pattern: Integrated IUnitOfWork and UnitOfWork to centralize access to StudentRepository and DepartmentRepository.
- Completed Student–Department Relationship: Connected Student and Department models with Foreign Keys, navigation properties, and dynamic dropdown selections.
- Full CRUD Operations: Updated controllers, services, and views to handle complete CRUD flows for both Students and Departments cleanly.
- Understood End-to-End Data Flow: Mastered how request data flows through Controller → Service → Unit of Work → Repository → Database.
- Atomic Unit of Work Transactions: Learned how Unit of Work to coordinate multiple entity updates (e.g., updating Student department while syncing department StudentCount) so all changes succeed or fail together.
- Replaced In-Memory Lists with Real Local DB: Integrated Entity Framework Core (ApplicationDbContext) to practice real database context persistence and SaveChanges() execution.
- 
### Issue # 50 Weekly Assignment - Routes & DI
- Implemented Attribute Routing (`[Route]`, `[HttpGet]`, `[HttpPost]`) across controllers for explicit URL mapping.
- Added route constraints (`:int`, `:min(1)`), default parameter values, and optional parameters for robust URL handling.
- Configured Named Routes (`Name = "..."`) to decouple link generation in Razor Views and controller redirects.
- Registered Repositories, Unit of Work, and Service dependencies in `Program.cs` using the IoC container (`AddScoped`).
- Decoupled controller logic by using Constructor Injection for `IStudentService` and `IDepartmentService`.

### Issue # 51 UI Improvements
- Implemented Bootstrap 5 for responsive and modern UI design.
- Added navigation bar, footer, and consistent layout across all pages.
- Improved UI in Student and Department Views with tables, forms, and validation messages.
- Made the application mobile-friendly and visually appealing.

### Issue # 52 - Bootstap Integration
- Right-clicked the project → Add → Client-Side Library.
- Selected cdnjs as the provider.
- Selected Bootstrap and its required files.
- Set the target location to wwwroot/lib/bootstrap.
- Added Bootstrap CSS and JS references in _Layout.cshtml.
- Used Bootstrap classes throughout the application for layout, navbar, cards, buttons, forms, tables, and responsive styling.
- Added Bootstrap Icons through CDN and used them across the UI.

### Issue # 53 - Refactoring
- StudentController: Moved repeated ViewBag code into a private LoadDepartments() helper method and inlined the GetById check directly within the if condition.
- StudentService: Created a centralized helper method to eliminate duplicate department count updates and improved the operation order in TransferDepartment().
- Repositories: Replaced FirstOrDefault with EF Core's Find() method to optimize primary key database lookups.
- UnitOfWork: Removed the redundant Save() method and standardized transaction completion on Complete().

### Issue #54 – Code Cleanup
- Removed unnecessary comments and redundant code from the controller and service layers.
- Cleaned up formatting and unused/redundant code while keeping the existing functionality unchanged.
- Removed the extra error model file related to the default ASP.NET Core MVC template.

### Issue #55 – Testing
- Created RepositoryPatternDemo.Tests using xUnit.
- Added Moq to mock repositories and Unit of Work.
- Implemented unit tests for StudentService methods.
- Covered success, failure, and edge-case scenarios.
- Implemented 23 test cases following the Arrange → Act → Assert pattern.
### Issue #56 – Bug Fixes
Fixed HTTP 405 errors in Edit and Delete routes.
Resolved EF Core tracking conflicts.
Fixed service and route mismatches.
Added null checks and explicit asp-route-id passing.
Verified the corrected Edit and Delete flows.

|-----------------------------------------------------------------|
|                                                                 |
|                    ### Issue #58 - README                       |
|                                                                 |
| --------------------------------------------------------------- |
|                                                                 |
| -> Create and update the `README.md` file.                      |
| -> Add a clear overview of the CMIntern4 project.               |
| -> Document the development environment and technologies used.  |
| -> Document the completed issues and project progress.          |
| -> Add the project structure.                                   |
| -> Add setup and run instructions.                              |
| -> Add testing information.                                     |
| -> Keep the README clear, organized, and easy to understand.    |
|                                                                 |
|-----------------------------------------------------------------|


## Project Structure

```
CMIntern4
└── FirstConsoleApplication
└── ControlStatements
└── Methods
└── Arrays-Collections
└── objects-Classes
└── Inheritance-Polymorphism
	├── Data
    ├── Helpers
    ├── Model
    ├── Services
    ├── UI
    ├── DTOs
    └── Program.cs
└── Encapsulation-Properties
	└── Model
	└── Services
	└── Program.cs
	└── Interfaces-Abstraction
    ├── Models
    ├── Interfaces
    ├── Services
    ├── Helpers
    ├── Configuration
    ├── Enums
    ├── UI
    └── Program.cs
└── AspNetCoreFundamentals
    ├── Connected Services
    ├── Dependencies
    ├── Properties
    ├── wwwroot
    ├── Configuration
    ├── Controllers
    ├── Filters
    ├── Interface
    ├── Middleware
    ├── Models
    ├── Service
    ├── Views
    ├── appsettings.json
    └── Program.cs
└── RepositoryPattern
    ├── Controllers/
    │   ├── DepartmentController.cs
    │   ├── HomeController.cs
    │   └── StudentController.cs
    ├── Data/
    │   └── ApplicationDbContext.cs
    ├── Models/
    │   ├── Department.cs
    │   ├── ErrorViewModel.cs
    │   └── Student.cs
    ├── Repositories/
    │   ├── Interfaces/
    │   │   ├── IDepartmentRepository.cs
    │   │   └── IStudentRepository.cs
    │   │   └── IUnitOfWork.cs
    │   ├── DepartmentRepository.cs
    │   └── StudentRepository.cs
    │   └── UnitOfWork.cs
    ├── Services/
    │   ├── Interfaces/
    │   │   ├── IDepartmentService.cs
    │   │   └── IStudentService.cs
    │   ├── DepartmentService.cs
    │   └── StudentService.cs
    ├── Views/
    │   ├── Department/
    │   ├── Home/
    │   ├── Student/
    │   └── Shared/
    ├── appsettings.json
    └── Program.cs


└── RepositoryPattern
    ├── Controllers/
    │   ├── DepartmentController.cs
    │   ├── HomeController.cs
    │   └── StudentController.cs
    ├── Data/
    │   └── ApplicationDbContext.cs
    ├── Models/
    │   ├── Department.cs
    │   ├── ErrorViewModel.cs
    │   └── Student.cs
    ├── Repositories/
    │   ├── Interfaces/
    │   │   ├── IDepartmentRepository.cs
    │   │   └── IStudentRepository.cs
    │   │   └── IUnitOfWork.cs
    │   ├── DepartmentRepository.cs
    │   └── StudentRepository.cs
    │   └── UnitOfWork.cs
    ├── Services/
    │   ├── Interfaces/
    │   │   ├── IDepartmentService.cs
    │   │   └── IStudentService.cs
    │   ├── DepartmentService.cs
    │   └── StudentService.cs
    ├── Views/
    │   ├── Department/
    │   ├── Home/
    │   ├── Student/
    │   └── Shared/
    ├── appsettings.json
    └── Program.cs
└── RepositoryPatternDemo.Tests
    ├── Tests
        ├──Services
            
        


```

```

```
### Setup
### Prerequisites

Make sure the following are installed:

Visual Studio 2026
- .NET SDK
- Git
### Clone the Repository
- git clone <repository-url>
### Open the Project

Open the solution in Visual Studio.

### Restore Dependencies

Visual Studio will restore the required NuGet packages automatically. You can also run:

dotnet restore
### Build the Project
dotnet build
Run the Application

Run the ASP.NET Core MVC application from Visual Studio or use:

dotnet run

Open the URL displayed in the terminal or Visual Studio.

### Run Tests

The project uses xUnit and Moq for unit testing.

Run all tests using:

dotnet test

The current test suite contains 23 StudentService unit tests covering successful operations, failures, and edge cases.

### Architecture

The Repository Pattern project follows this flow:

HTTP Request
     ↓
Controller
     ↓
Service
     ↓
Unit of Work
     ↓
Repository
     ↓
Entity Framework Core
     ↓
Database


This structure separates responsibilities and makes the application easier to maintain, test, and extend.

### Project Status

## Status: In Progress

The project currently contains completed C# fundamentals, ASP.NET Core MVC fundamentals, Repository Pattern implementation, Unit of Work, Dependency Injection, Bootstrap integration, refactoring, code cleanup, unit testing, and bug fixes.

Further development will continue according to the internship training roadmap.
