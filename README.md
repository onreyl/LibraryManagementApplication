#  Library Management System - Backend API

A RESTful API for library management system built with **ASP.NET Core 8.0** following **Clean Architecture** principles.

## Features

- **Clean Architecture** (Domain, Application, Infrastructure, API layers)
- **JWT Authentication** (Login, Register, Token-based auth)
- **Repository Pattern** with Entity Framework Core
- **CRUD Operations** for Books, Authors, Categories, Users, Borrow Records
- **AutoMapper** for object mapping
- **Swagger UI** with JWT support
- **Unit Tests** with xUnit and Moq
- **Exception Handling Middleware**
- **CORS** enabled

## Technologies

- **Framework:** ASP.NET Core 8.0
- **Database:** SQL Server with Entity Framework Core
- **Authentication:** JWT Bearer Token
- **Testing:** xUnit, Moq
- **Documentation:** Swagger/OpenAPI
- **Password Hashing:** BCrypt

## Project Structure

```
LibraryManagementApplication/
├── LibraryManagement.Domain/          # Entities, Interfaces
├── LibraryManagement.Application/     # Services, DTOs, Business Logic
├── LibraryManagement.Infrastructure/  # Data Access, Repositories
├── LibraryManagement.API/             # Controllers, Middleware
└── LibraryManagement.Tests/           # Unit Tests
```

## Setup & Installation

### Prerequisites
- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Installation Steps

1. **Clone the repository**
```bash
git clone https://github.com/YOUR_USERNAME/LibraryManagementApplication.git
cd LibraryManagementApplication
```

2. **Update Connection String**

Edit `LibraryManagement.API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=LibraryManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "SecretKey": "YOUR_SECRET_KEY_HERE_AT_LEAST_32_CHARACTERS!",
    "Issuer": "LibraryManagementAPI",
    "Audience": "LibraryManagementClient",
    "ExpirationMinutes": 60
  }
}
```

3. **Run Migrations**
```bash
cd LibraryManagement.Infrastructure
dotnet ef database update --startup-project ../LibraryManagement.API
```

4. **Run the Application**
```bash
cd ../LibraryManagement.API
dotnet run
```

5. **Access Swagger UI**
```
http://localhost:5000/swagger
```

## Running Tests

```bash
cd LibraryManagement.Tests
dotnet test
```

## Authentication

### Register a new user
```http
POST /api/auth/register
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com",
  "phoneNumber": "1234567890",
  "password": "password123"
}
```

### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "john@example.com",
  "password": "password123"
}
```

### Use Token
Add the token to request headers:
```
Authorization: Bearer YOUR_JWT_TOKEN
```


