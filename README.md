# CMIntern4

## Development Environment

- Visual Studio 2022 Installed
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
	

=======
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
- Improved code reusability and maintainability using helper classes and common validation logic.

#### -------------- Start ASP.NET Core Fundamentals ----------------- ###

### Issue #15 - Project Setup
- Created a new ASP.NET Core MVC solution and project.
- Configured the project using the .NET framework.
- Explored the basic ASP.NET Core MVC project structure.
- Built and ran the application successfully.
- Verified the default ASP.NET Core MVC application in the browser.
- Prepared a working project skeleton for upcoming ASP.NET Core Fundamentals issues.

### Issue #16 - Project Structure
- Organized the project using the default ASP.NET Core MVC folder structure.
- Verified the `Controllers`, `Models`, and `Views` folders following ASP.NET Core conventions.
- Reviewed the purpose and responsibility of each folder.
- Maintained a clean and organized project structure for future development.

### Issue #17 - Program.cs
- Reviewed the `Program.cs` file as the application's entry point.
- Understood the application startup flow, including host creation, service registration, and application initialization.
- Verified the default MVC service registration using `AddControllersWithViews()`.
- Reviewed the HTTP request pipeline and default route configuration.
- Built and ran the application successfully without errors.

### Issue #18 - Configuration
- Reviewed the ASP.NET Core configuration system.
- Explored the `appsettings.json` and `appsettings.Development.json` files.
- Added custom application settings in `appsettings.json`.
- Read configuration values in `Program.cs` using `builder.Configuration`.
- Verified that configuration values were loaded successfully at runtime.

### Issue #19 - Middleware
- Reviewed the ASP.NET Core middleware pipeline and request flow.
- Explored the built-in middleware configured in `Program.cs`.
- Implemented a custom middleware to measure HTTP request processing time.
- Logged the request path and execution time for each incoming request.
- Verified that the middleware executed successfully for every request.
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





```

## Getting started

To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.vteamslabs.com/dotnet/trainings/cmintern4.git
git branch -M main
git push -uf origin main
```

## Integrate with your tools

- [ ] [Set up project integrations](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern4/-/settings/integrations)

## Collaborate with your team

- [ ] [Invite team members and collaborators](https://docs.gitlab.com/ee/user/project/members/)
- [ ] [Create a new merge request](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)
- [ ] [Automatically close issues from merge requests](https://docs.gitlab.com/ee/user/project/issues/managing_issues.html#closing-issues-automatically)
- [ ] [Enable merge request approvals](https://docs.gitlab.com/ee/user/project/merge_requests/approvals/)
- [ ] [Automatically merge when pipeline succeeds](https://docs.gitlab.com/ee/user/project/merge_requests/merge_when_pipeline_succeeds.html)

## Test and Deploy

Use the built-in continuous integration in GitLab.

- [ ] [Get started with GitLab CI/CD](https://docs.gitlab.com/ee/ci/quick_start/index.html)
- [ ] [Analyze your code for known vulnerabilities with Static Application Security Testing(SAST)](https://docs.gitlab.com/ee/user/application_security/sast/)
- [ ] [Deploy to Kubernetes, Amazon EC2, or Amazon ECS using Auto Deploy](https://docs.gitlab.com/ee/topics/autodevops/requirements.html)
- [ ] [Use pull-based deployments for improved Kubernetes management](https://docs.gitlab.com/ee/user/clusters/agent/)
- [ ] [Set up protected environments](https://docs.gitlab.com/ee/ci/environments/protected_environments.html)

***

# Editing this README

When you're ready to make this README your own, just edit this file and use the handy template below (or feel free to structure it however you want - this is just a starting point!).  Thank you to [makeareadme.com](https://www.makeareadme.com/) for this template.

## Suggestions for a good README
Every project is different, so consider which of these sections apply to yours. The sections used in the template are suggestions for most open source projects. Also keep in mind that while a README can be too long and detailed, too long is better than too short. If you think your README is too long, consider utilizing another form of documentation rather than cutting out information.

## Name
Choose a self-explaining name for your project.

## Description
Let people know what your project can do specifically. Provide context and add a link to any reference visitors might be unfamiliar with. A list of Features or a Background subsection can also be added here. If there are alternatives to your project, this is a good place to list differentiating factors.

## Badges
On some READMEs, you may see small images that convey metadata, such as whether or not all the tests are passing for the project. You can use Shields to add some to your README. Many services also have instructions for adding a badge.

## Visuals
Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Installation
Within a particular ecosystem, there may be a common way of installing things, such as using Yarn, NuGet, or Homebrew. However, consider the possibility that whoever is reading your README is a novice and would like more guidance. Listing specific steps helps remove ambiguity and gets people to using your project as quickly as possible. If it only runs in a specific context like a particular programming language version or operating system or has dependencies that have to be installed manually, also add a Requirements subsection.

## Usage
Use examples liberally, and show the expected output if you can. It's helpful to have inline the smallest example of usage that you can demonstrate, while providing links to more sophisticated examples if they are too long to reasonably include in the README.

## Support
Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap
If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing
State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment
Show your appreciation to those who have contributed to the project.

## License
For open source projects, say how it is licensed.

## Project status
If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing your project to keep going. You can also make an explicit request for maintainers.
