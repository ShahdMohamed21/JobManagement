# Job Management API

A production-oriented **ASP.NET Core Web API** for managing jobs, candidates, recruiters, job applications, authentication, authorization, administration, and background processing.

The project is built using **Clean Architecture principles**, **CQRS**, **MediatR**, **Entity Framework Core**, **ASP.NET Core Identity**, **JWT Authentication**, **Role-Based Authorization**, and **Hangfire**.

---

## 🚀 Features

### 🔐 Authentication & Authorization

* User registration and login
* JWT-based authentication
* Role-Based Authorization
* Three main roles:

  * **Admin**
  * **Recruiter**
  * **Candidate**
* Secure password handling through ASP.NET Core Identity
* Protected API endpoints
* User activation/deactivation

---

### 👤 User Management

Admins can:

* View all registered users
* View user roles
* Activate users
* Deactivate users

User management is implemented through Identity while keeping Identity-specific logic inside the Infrastructure layer.

---

### 💼 Job Management

Recruiters can:

* Create jobs
* Update their own jobs
* Soft-delete their own jobs
* Define an optional expiry date

Users can:

* View active jobs
* View a specific active job

### Job Expiration

Jobs can have an optional:

```text
ExpiryDate
```

When a job expires, it is automatically deactivated by a Hangfire recurring job.

---

### 📄 Job Applications

Candidates can:

* Apply for jobs
* View their applications
* Cancel their applications

Recruiters can:

* View applications submitted to their jobs
* Update application status

Supported application statuses include:

* Applied
* UnderReview
* Interview
* Accepted
* Rejected
* Cancelled

Business rules are enforced to prevent invalid application operations.

---

## 🏗️ Architecture

The project follows **Clean Architecture / Layered Architecture principles**.

```text
JobManagement
│
├── JobManagement.API
│
├── JobManagement.Application
│
├── JobManagement.Domain
│
└── JobManagement.Infrastructure
```

### Dependency Flow

```text
API
 ↓
Application
 ↓
Domain

Infrastructure
 ↓
Application
 ↓
Domain
```

The Application layer does not depend directly on Infrastructure implementations.

Infrastructure implements the interfaces defined by Application.

This keeps the business logic independent from infrastructure concerns such as:

* Database access
* Entity Framework Core
* ASP.NET Identity
* Hangfire
* External services

---

# 🧱 Project Structure

## JobManagement.Domain

Contains the core business entities and enums.

Examples:

```text
Entities
├── Job
├── JobApplication
└── Notification

Enums
└── ApplicationStatus
```

The Domain layer does not depend on other application layers.

---

## JobManagement.Application

Contains application logic, DTOs, interfaces, and CQRS features.

```text
Application
│
├── DTOs
│
├── Interfaces
│   └── Repositories
│
└── Features
    ├── Jobs
    ├── JobApplications
    └── Admin
```

---

## JobManagement.Infrastructure

Contains implementations of infrastructure concerns.

```text
Infrastructure
│
├── Data
│
├── Identity
│
├── Repositories
│
└── Services
```

Examples:

* Entity Framework Core
* SQL Server
* ASP.NET Identity
* Repository implementations
* JWT token generation
* Hangfire services
* Notification services

---

## JobManagement.API

Contains:

* Controllers
* Dependency Injection configuration
* Authentication configuration
* Swagger configuration
* Middleware pipeline
* Hangfire Dashboard

---

# 🔄 CQRS

The project uses **CQRS (Command Query Responsibility Segregation)**.

Commands are responsible for operations that modify data.

Examples:

```text
CreateJobCommand
UpdateJobCommand
DeleteJobCommand

CreateApplicationCommand
CancelApplicationCommand
UpdateApplicationStatusCommand

UpdateUserStatusCommand
```

Queries are responsible for retrieving data.

Examples:

```text
GetAllJobsQuery
GetJobByIdQuery
GetMyApplicationsQuery
GetRecruiterApplicationsQuery
GetUsersQuery
```

This separates read and write operations and keeps application features organized.

---

# 📨 MediatR

**MediatR** is used to implement the CQRS request/handler pattern.

Example:

```text
Controller
    ↓
MediatR
    ↓
Command / Query
    ↓
Handler
    ↓
Repository
    ↓
Database
```

Controllers remain thin and delegate application operations to MediatR handlers.

---

# 🗄️ Entity Framework Core

The project uses **Entity Framework Core** with SQL Server.

EF Core is responsible for:

* Database access
* Entity configuration
* Relationships
* Migrations
* LINQ queries
* Tracking / NoTracking queries

The project uses explicit Entity Configurations for entities and relationships.

---

# 🔑 ASP.NET Core Identity

ASP.NET Core Identity is used for user management.

The system supports:

```text
Admin
Recruiter
Candidate
```

Identity manages:

* Users
* Passwords
* Roles
* Authentication-related data

Custom user properties include:

```text
CVUrl
IsActive
```

---

# 🪪 JWT Authentication

The API uses JWT Bearer Authentication.

Authentication flow:

```text
Login
  ↓
Validate Email + Password
  ↓
Get User Roles
  ↓
Generate JWT
  ↓
Client sends:
Authorization: Bearer <token>
```

Role-based authorization is applied to protected endpoints.

Example:

```csharp
[Authorize(Roles = "Recruiter")]
```

---

# 🔐 Authorization Rules

### Candidate

Can:

* Apply for jobs
* Cancel applications
* View own applications

### Recruiter

Can:

* Create jobs
* Update own jobs
* Delete own jobs
* View applications for their jobs
* Update application statuses

### Admin

Can:

* View users
* Activate/deactivate users

---

# ⚙️ Hangfire

The project uses **Hangfire** for background processing.

Hangfire is configured with SQL Server storage.

```text
ASP.NET Core API
       ↓
    Hangfire
       ↓
   SQL Server
```

## Recurring Jobs

A daily recurring job automatically closes expired jobs:

```text
close-expired-jobs
```

It executes:

```text
CloseExpiredJobsAsync()
```

The service checks for active jobs where:

```text
ExpiryDate <= current time
```

and changes:

```text
IsActive = false
```

This allows expired jobs to be automatically removed from the active job listings.

---

## Background Notifications

Hangfire is also used for background notification processing.

When a candidate applies for a job:

```text
Candidate
   ↓
Create Application
   ↓
Enqueue Background Job
   ↓
Hangfire
   ↓
NotificationService
   ↓
Notifications Table
```

This keeps background work separate from the main HTTP request.

---

# 🔔 Notifications

The project contains a notification system for background processing.

Notifications include:

```text
UserId
Message
IsRead
CreatedAt
```

Notifications are stored in SQL Server and can be generated through background jobs.

---

# 🧩 Repository Pattern

Repositories are defined as interfaces in the Application layer.

Example:

```csharp
IJobRepository
IJobApplicationRepository
IUserRepository
INotificationRepository
```

Their implementations exist in Infrastructure.

```text
Application
    ↓
IJobRepository
    ↓
Infrastructure
    ↓
JobRepository
```

This keeps the Application layer independent from EF Core implementation details.

---

# 🛡️ Business Rules

The API implements several business rules, including:

* Only recruiters can create jobs.
* Recruiters can modify only their own jobs.
* Recruiters can view applications for their own jobs.
* Candidates can manage their own applications.
* Cancelled applications cannot be changed by recruiters.
* Only authorized roles can access protected endpoints.
* Expired jobs are automatically deactivated.
* Users can be activated/deactivated by administrators.

---

# 🗃️ Database

The application uses:

```text
Microsoft SQL Server
```

Entity Framework Core migrations are used to manage database schema changes.

Example:

```powershell
Add-Migration MigrationName `
    -Project JobManagement.Infrastructure `
    -StartupProject JobManagement.API
```

Update the database:

```powershell
Update-Database `
    -Project JobManagement.Infrastructure `
    -StartupProject JobManagement.API
```

---

# 📚 API Endpoints

## Authentication

```text
POST /api/Auth/Register
POST /api/Auth/Login
GET  /api/Auth/Test
```

---

## Jobs

```text
GET    /api/Jobs
GET    /api/Jobs/{id}

POST   /api/Jobs
PUT    /api/Jobs/{id}
DELETE /api/Jobs/{id}
```

Create, update, and delete operations require the **Recruiter** role.

---

## Job Applications

```text
POST   /api/JobApplications
DELETE /api/JobApplications/{id}

GET    /api/JobApplications/my
GET    /api/JobApplications/recruiter

PUT    /api/JobApplications/{id}/status
```

---

## Admin

```text
GET /api/Admin/users

PUT /api/Admin/users/{id}/status
```

Admin endpoints require the **Admin** role.

---

# 📖 Swagger

The API includes Swagger/OpenAPI documentation.

Swagger provides:

* Endpoint documentation
* Request/response schemas
* JWT authorization
* API testing

After running the project, open:

```text
/swagger
```

Use the **Authorize** button to provide:

```text
Bearer <JWT Token>
```

---

# 📊 Hangfire Dashboard

Hangfire Dashboard is available at:

```text
/hangfire
```

It provides visibility into:

* Recurring Jobs
* Enqueued Jobs
* Processing Jobs
* Succeeded Jobs
* Failed Jobs
* Servers
* Scheduled Jobs

---

# 🧪 Testing Flow

A typical application flow:

```text
1. Register Candidate
        ↓
2. Register Recruiter
        ↓
3. Login Recruiter
        ↓
4. Recruiter creates Job
        ↓
5. Login Candidate
        ↓
6. Candidate views Jobs
        ↓
7. Candidate applies
        ↓
8. Application is created
        ↓
9. Background notification is queued
        ↓
10. Recruiter reviews application
        ↓
11. Recruiter updates status
```

---

# 🧰 Technologies

| Technology            | Purpose                 |
| --------------------- | ----------------------- |
| ASP.NET Core          | Web API                 |
| C#                    | Programming Language    |
| Entity Framework Core | ORM                     |
| SQL Server            | Database                |
| ASP.NET Core Identity | User Management         |
| JWT                   | Authentication          |
| MediatR               | CQRS / Request Handling |
| Hangfire              | Background Processing   |
| Swagger / OpenAPI     | API Documentation       |
| LINQ                  | Data Querying           |

---

# 🎯 Architecture & Design Patterns

The project demonstrates several software engineering concepts:

* Clean Architecture
* Layered Architecture
* CQRS
* Mediator Pattern
* Repository Pattern
* Dependency Injection
* DTO Pattern
* Role-Based Authorization
* Background Processing
* Recurring Jobs
* Soft Delete
* Entity Configuration
* Database Migrations

---

# 🚀 Running the Project

### 1. Clone the repository

```bash
git clone <repository-url>
```

### 2. Configure the connection string

Update:

```text
appsettings.json
```

with your SQL Server connection string.

### 3. Configure JWT

Set the required JWT settings:

```json
"Jwt": {
  "Key": "your-secret-key",
  "Issuer": "your-issuer",
  "Audience": "your-audience"
}
```

### 4. Apply migrations

```powershell
Update-Database `
    -Project JobManagement.Infrastructure `
    -StartupProject JobManagement.API
```

### 5. Run the API

```bash
dotnet run
```

### 6. Open Swagger

```text
/swagger
```

### 7. Open Hangfire Dashboard

```text
/hangfire
```

---

# 📌 Project Goals

This project was designed to demonstrate how to build a structured, scalable, and maintainable backend application using modern ASP.NET Core practices.

The main focus areas are:

* Clean separation of responsibilities
* Maintainable architecture
* Secure authentication and authorization
* Database-driven business logic
* CQRS-based application features
* Background job processing
* Real-world business rules
* API documentation
* Production-oriented backend practices

---

## 👩‍💻 Author

**Shahd Mohamed**

ASP.NET Core Backend Developer
