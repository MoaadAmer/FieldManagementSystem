
# Field Management System API

## Overview
The Field Management System is an ASP.NET Core Web API backend. It provides role-based access and CRUD operations for managing agricultural fields, users, and controllers. The system uses ADO.NET with stored procedures for database interactions.

## Roles
- **Admin**: Manages users and roles.
- **Farmer**: Manages fields and controllers.
- **Agronomist**: View only fields and controllers data.

## Features
- User management (Add, Get by ID/Email, Update, Delete)
- Field management (Add, Get by ID/User, Update, Delete)
- Controller management (Add, Get by ID/Field, Delete)
- Role-based authorization using JWT

## Technologies
- ASP.NET Core Web API (.NET 8)
- ADO.NET with SQL Server stored procedures
- SQL Server

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- SQL Server
- Visual Studio or VS Code

### Steps
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/FieldManagementSystem.git
   ```

2. Open the solution in Visual Studio.

3. Configure the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "Default": "Server=(localdb)\\MSSQLLocalDB;Database=FieldManagementSystemDB;User Id={addYourUser};Integrated Security=true;"
   }
   ```

4. Restore NuGet packages:
   ```bash
   dotnet restore
   ```

5. Build and run the project:
   ```bash
   dotnet build
   dotnet run
   ```

## Database Setup

### Using SQL Script
- Navigate to the `Database/` folder.
- Open `script.sql` in SSMS.
- Execute the script to create tables and stored procedures.


## API Endpoints

### Auth
- `POST /api/auth` — Login with email to receive JWT token

### Users
- `POST /api/users`
- `GET /api/users`
- `GET /api/users/id/{id}`
- `GET /api/users/email/{email}`
- `PUT /api/users/{id}`
- `DELETE /api/users/{id}`

### Fields
- `POST /api/fields`
- `GET /api/fields/{id}`
- `GET /api/fields/user/{userId}`
- `PUT /api/fields/{id}`
- `DELETE /api/fields/{id}`

### Controllers
- `POST /api/controllers`
- `GET /api/controllers/{id}`
- `GET /api/controllers/field/{fieldId}`
- `DELETE /api/controllers/{id}`

## Notes
- Tests are not included in this version.

## Author
Moaad Amer
