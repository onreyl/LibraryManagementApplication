# 📚 Library Management System - Backend API

A RESTful API for library management system built with **ASP.NET Core 8.0** following **Clean Architecture** principles.

## 🚀 Features

- ✅ **Clean Architecture** (Domain, Application, Infrastructure, API layers)
- ✅ **JWT Authentication** (Login, Register, Token-based auth)
- ✅ **Repository Pattern** with Entity Framework Core
- ✅ **CRUD Operations** for Books, Authors, Categories, Users, Borrow Records
- ✅ **AutoMapper** for object mapping
- ✅ **Swagger UI** with JWT support
- ✅ **Unit Tests** with xUnit and Moq
- ✅ **Exception Handling Middleware**
- ✅ **CORS** enabled

## 🛠️ Technologies

- **Framework:** ASP.NET Core 8.0
- **Database:** SQL Server with Entity Framework Core
- **Authentication:** JWT Bearer Token
- **Testing:** xUnit, Moq
- **Documentation:** Swagger/OpenAPI
- **Password Hashing:** BCrypt

## 📁 Project Structure

```
LibraryManagementApplication/
├── LibraryManagement.Domain/          # Entities, Interfaces
├── LibraryManagement.Application/     # Services, DTOs, Business Logic
├── LibraryManagement.Infrastructure/  # Data Access, Repositories
├── LibraryManagement.API/             # Controllers, Middleware
└── LibraryManagement.Tests/           # Unit Tests
```

## ⚙️ Setup & Installation

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

## 🧪 Running Tests

```bash
cd LibraryManagement.Tests
dotnet test
```

## 🔐 Authentication

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

## 📚 API Endpoints

### Authentication
- `POST /api/auth/register` - Register new user
- `POST /api/auth/login` - Login and get JWT token

### Books
- `GET /api/book` - Get all books
- `GET /api/book/{id}` - Get book by ID
- `POST /api/book` - Create new book
- `PUT /api/book/{id}` - Update book
- `DELETE /api/book/{id}` - Delete book

### Authors
- `GET /api/author` - Get all authors
- `GET /api/author/{id}` - Get author by ID
- `POST /api/author` - Create new author
- `PUT /api/author/{id}` - Update author
- `DELETE /api/author/{id}` - Delete author

### Categories
- `GET /api/category` - Get all categories
- `GET /api/category/{id}` - Get category by ID
- `POST /api/category` - Create new category
- `PUT /api/category/{id}` - Update category
- `DELETE /api/category/{id}` - Delete category

### Users
- `GET /api/user` - Get all users
- `GET /api/user/{id}` - Get user by ID
- `POST /api/user` - Create new user
- `PUT /api/user/{id}` - Update user
- `DELETE /api/user/{id}` - Delete user

### Borrow Records
- `GET /api/borrowrecord` - Get all borrow records
- `GET /api/borrowrecord/{id}` - Get borrow record by ID
- `POST /api/borrowrecord` - Create new borrow record
- `PUT /api/borrowrecord/{id}` - Update borrow record
- `DELETE /api/borrowrecord/{id}` - Delete borrow record

## 👨‍💻 Author

**Your Name**
- GitHub: [@yourusername](https://github.com/yourusername)
- LinkedIn: [Your LinkedIn](https://linkedin.com/in/yourprofile)

## 📄 License

This project is licensed under the MIT License.
