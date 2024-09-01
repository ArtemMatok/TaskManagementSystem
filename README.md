# Task Management System

## Overview

This project is a Task Management System built using .NET 8 and Entity Framework. It includes functionality for user authentication, task creation, updating, deletion, and retrieval. The application uses JWT for authorization and has a RESTful API for interaction.

## Setup Instructions

### Prerequisites

- .NET 8 SDK
- SQL Server

### Running the Project Locally

1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/task-management-system.git
   cd task-management-system
  

### Configure the database
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=yourserver;Database=yourdatabase;User Id=yourusername;Password=yourpassword;"
}
````


### Apply Migrations
```bash
dotnet ef database update
```
### Run application



## API Documentation

### User Endpoing


### Register User
- URL: /api/users/Register
- Method: POST
- Request Body
```bash
{
  "UserName": "string",
  "Email": "string",
  "Password": "string"
}
```

- Response:
  - Success (200 OK):
  ```bash
  {
    "Username": "string",
    "Email": "string",
    "Token": "string"
  }
  ```
  - Error (400 Bad Request / 500 Internal Server Error): Returns error details.

### Login User
- URL: /api/users/Login
- Method: POST
- Request Body:
```bash
{
  "Email": "string",
  "Password": "string"
}
```

- Response
  - Success (200 OK)
  ```bash
  {
    "Username": "string",
    "Email": "string",
    "Token": "string"
  }
  ```
  - Error (400 Bad Request / 401 Unauthorized): Returns error details.

## Task Endpoints

### Create Task

- URL: /api/tasks
- Method: POST
- Request Body:
```bash
{
  "Title": "string",
  "Description": "string",
  "DueDate": "2024-01-01T00:00:00Z"
}
```
- Response:
  - Success (200 OK):
  {
    "TaskId": "guid",
    "Title": "string",
    "Description": "string",
    "DueDate": "2024-01-01T00:00:00Z"
  }
  - Error (400 Bad Request / 401 Unauthorized / 404 Not Found): Returns error details.

### Update Task

- URL: /api/tasks/{taskId}
- Method: PUT
- Request Body:
```basg
{
  "Title": "string",
  "Description": "string",
  "DueDate": "2024-01-01T00:00:00Z"
}
```
- Response:
  - Success (200 OK):
  {
    "TaskId": "guid",
    "Title": "string",
    "Description": "string",
    "DueDate": "2024-01-01T00:00:00Z"
  }
  - Error (400 Bad Request / 401 Unauthorized / 404 Not Found): Returns error details.

### Delete Task
- URL: /api/tasks/{taskId}
- Method: DELETE
- Response:
  - Success (200 OK):
  ```bash
  "Deleted"
  ```
  - Error (400 Bad Request / 401 Unauthorized / 404 Not Found): Returns error details.

### Get Tasks
- URL: /api/tasks
- Method: GET
- Query Parameters:
  - pageNumber: Page number for pagination (default: 1)
  - pageSize: Number of items per page (default: 10)
- Response:
  - Success (200 OK):
  ```bash
  [
    {
      "TaskId": "guid",
      "Title": "string",
      "Description": "string",
      "DueDate": "2024-01-01T00:00:00Z"
    }
  ]
  ```
  - Error (401 Unauthorized): Returns error details.
 
### Get Task By Id
- URL: /api/tasks/{taskId}
- Method: GET
- Response:
  - Success (200 OK):
  ```bash
  {
    "TaskId": "guid",
    "Title": "string",
    "Description": "string",
    "DueDate": "2024-01-01T00:00:00Z"
  }
  - Error (401 Unauthorized / 404 Not Found): Returns error details.

## Architecture and Design Choices

### Architecture
-The application follows a clean architecture pattern, separating concerns into Controllers, Services, and Repositories.
  -Controllers handle HTTP requests and responses.
  -Services contain business logic.
  -Repositories interact with the database.
  
## Authentication
- Uses ASP.NET Core Identity for user management and JWT for secure API access.
- All task-related endpoints require authentication, ensuring only authorized users can create, update, delete, or view tasks.

## Validation
- Input validation is performed at both the model and controller levels to ensure data integrity and provide meaningful error messages.
