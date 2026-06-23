# Leave Management System

## Overview

Leave Management System is a full-stack web application developed using ASP.NET Core Web API and PostgreSQL. It allows employees to apply for leaves and administrators to manage leave requests through approval and rejection workflows.

The project is designed using layered architecture and follows industry-standard practices such as JWT Authentication, Role-Based Authorization, Global Exception Handling, and Entity Framework Core Code First approach.

---

## Tech Stack

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* BCrypt Password Hashing
* Swagger

### Database

* PostgreSQL
* pgAdmin

### Frontend

* Angular (Under Development)

---

## Features

### Authentication

* User Registration
* User Login
* JWT Token Generation
* BCrypt Password Hashing

### Authorization

* Role-Based Authorization
* Admin Role
* Employee Role

### Employee Features

* Apply Leave
* View My Leaves
* View Leave Balance

### Admin Features

* View All Leave Requests
* Approve Leave Request
* Reject Leave Request
* Dashboard Statistics

### Additional Features

* Global Exception Middleware
* Leave Balance Management
* Leave Status Tracking
* Duplicate Leave Prevention
* Leave Balance Deduction on Approval

---

## Database Tables

### Users

* Id
* FullName
* Email
* PasswordHash
* RoleId

### Roles

* Id
* RoleName

### LeaveTypes

* Id
* Name

### LeaveRequests

* Id
* UserId
* LeaveTypeId
* FromDate
* ToDate
* Reason
* Status
* AppliedDate
* CreatedAt
* UpdatedAt

### LeaveBalances

* Id
* UserId
* SickLeave
* CasualLeave
* EarnedLeave

---

## API Endpoints

### Authentication

POST /api/Auth/register

POST /api/Auth/login

---

### Leave

POST /api/leave/apply

GET /api/leave/my-leaves

GET /api/leave/balance

---

### Admin

GET /api/leave/all

PUT /api/leave/approve/{id}

PUT /api/leave/reject/{id}

GET /api/dashboard

---

## Authentication

The application uses JWT Bearer Authentication.

After login:

1. Copy the JWT token.
2. Click the Authorize button in Swagger.
3. Enter:

Bearer YOUR_TOKEN

4. Access protected APIs.

---

## Run the Project

### Clone Repository

git clone YOUR_GITHUB_REPOSITORY_URL

### Navigate to API Project

cd leave-management-api

### Update Connection String

Update appsettings.json:

Host=localhost;
Port=5432;
Database=LeaveManagementDB;
Username=postgres;
Password=YourPassword;

### Apply Migrations

Update-Database

### Run Project

Press F5

or

dotnet run

### Swagger

[https://localhost:xxxx/swagger](https://localhost:xxxx/swagger)

---

## Project Architecture

Controllers

DTOs

Models

Services

Interfaces

Middleware

Data

Enums

Migrations

---

## Future Enhancements

* Angular Frontend
* Admin Dashboard Charts
* Email Notifications
* Pagination and Filtering
* Docker Support
* Deployment on Cloud

---

## Author

Pranav Rajendra Deshmukh

Junior Software Engineer

ASP.NET Core | Angular | PostgreSQL | Entity Framework Core
