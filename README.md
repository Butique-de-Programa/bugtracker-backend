# BugTracker - Backend (ASP.NET Core)

## 🚀 Why C# / .NET?

- Mature ecosystem with first-class support for dependency injection and design patterns.
- Excellent tooling (Visual Studio / VS Code).
- Tight integration with PostgreSQL (as used by Supabase).

## 🔧 Libraries & Patterns

| Concern                      | Library / Pattern              | Purpose                                                   |
|-----------------------------|--------------------------------|-----------------------------------------------------------|
| Web Framework                | ASP.NET Core MVC / Minimal API | Routing and endpoints                                     |
| Dependency Injection         | Built-in DI Container          | Loose coupling and testability                            |
| Data Access                  | Entity Framework Core (EF Core)| Migrations and database access for PostgreSQL             |
| Repository & Unit of Work    | Custom Repos + UnitOfWork      | Encapsulate queries, improve testability                  |
| Auth & Authorization         | ASP.NET Identity + JWT Bearer  | Secure login, role-based access, token issuance           |
| Input Validation             | FluentValidation               | Strong and reusable request validation                    |
| Object Mapping               | AutoMapper                     | Reduce boilerplate when mapping between DTOs/entities     |
| Logging                      | Serilog + Seq / console sink   | Structured logs for debugging and monitoring              |
| Error Handling               | Custom Middleware              | Centralized exception formatting and response structure   |

## 📐 MVP Scope

### A. Project Setup

1. Scaffold a Web API project  
2. Add EF Core with PostgreSQL provider  
3. Initialize DB schema:
   - `User`: Id, Email, PasswordHash, Role
   - `Project`: Id, Name, Description
   - `Issue`: Id, Title, Description, Status, Priority, CreatedBy, AssignedTo, Timestamps

### B. Auth & JWT

- Register/login with ASP.NET Identity  
- Issue and validate JWTs  
- Use `[Authorize]` attributes and role policies

### C. Core API Features

- `POST /api/issues` → create issue  
- `GET /api/issues` → list with filters  
- `PUT /api/issues/{id}` → update issue  
- `DELETE /api/issues/{id}` → delete or archive

### D. Bonus

- Swagger for testing  
- Serilog for logging  
- Docker support (optional)


How to run:

## 💾 Docker
```bash
docker-compose up -d
