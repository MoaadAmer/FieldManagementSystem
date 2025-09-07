# 🌾 Field Management System API

## Overview
The **Field Management System** is a backend API built with **ASP.NET Core (.NET 8)** that enables role-based management of agricultural data. It supports secure CRUD operations for **Users**, **Fields**, and **Controllers**, using **JWT authentication** and **ADO.NET with SQL Server stored procedures**.

## 👥 Roles & Permissions

| Role        | Permissions                                      |
|-------------|--------------------------------------------------|
| **Admin**   | Manage users (create, update, delete, view)      |
| **Farmer**  | Manage their own fields and controllers          |
| **Agronomist** | View-only access to fields and controllers     |

## ✨ Features

- 🔐 **JWT-based authentication and role-based authorization**
- 👤 **User management**: Add, retrieve, update, delete users
- 🌱 **Field management**: Add, retrieve, update, delete fields
- 📟 **Controller management**: Add, retrieve, delete controllers
- 🛡️ **Secure database access** using ADO.NET and stored procedures

## 🛠️ Technologies Used

- **ASP.NET Core Web API (.NET 8)**
- **ADO.NET** with **SQL Server stored procedures**
- **JWT Authentication**
- **Swagger/OpenAPI** for API documentation

## 🚀 Getting Started

### ✅ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server (LocalDB or full instance)
- Visual Studio or VS Code

### 📥 Clone the Repository
```bash
git clone https://github.com/MoaadAmer/FieldManagementSystem.git
```

### ⚙️ Configure the Connection String
In `appsettings.json`:
```json
"ConnectionStrings": {
  "Default": "Server=(localdb)\MSSQLLocalDB;Database=FieldManagementSystemDB;User Id={yourUser};Integrated Security=true;"
}
```

### 📦 Restore Dependencies
```bash
dotnet restore
```

### 🏗️ Build and Run the Project
```bash
dotnet build
dotnet run
```

## 🗃️ Database Setup

### Option 1: Using SQL Script
1. Navigate to the `Database/` folder.
2. Open `script.sql` in SQL Server Management Studio (SSMS).
3. Execute the script to create tables and stored procedures.

## 📡 API Endpoints

### 🔐 Auth
- `POST /api/auth` — Login with email to receive JWT token

### 👤 Users (Admin only)
- `POST /api/users`
- `GET /api/users`
- `GET /api/users/id/{id}`
- `GET /api/users/email/{email}`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`

### 🌱 Fields
- `POST /api/fields`
- `GET /api/fields/{id}`
- `GET /api/fields/user/{userId}`
- `PUT /api/fields/{id}`
- `DELETE /api/fields/{id}`

### 📟 Controllers
- `POST /api/controllers`
- `GET /api/controllers/{id}`
- `GET /api/controllers/field/{fieldId}`
- `DELETE /api/controllers/{id}`

## 🧪 Notes
- ✅ JWT authentication is implemented.
- ❌ Unit tests are not included in this version (planned for future).
- 🔄 Future improvements: refresh tokens, frontend integration, test coverage.

## 👨‍💻 Author
[**Moaad Amer**](https://github.com/MoaadAmer)
