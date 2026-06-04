# Dynamic Form Builder – .NET 8 Full Stack Assessment

## Overview

Dynamic Form Builder is a full-stack application built using .NET 8, ASP.NET Core Web API, Entity Framework Core, SQL Server, JWT Authentication, and ASP.NET Core MVC.

The application dynamically generates forms based on configuration stored in the database. New fields, validations, dropdown values, and form layouts can be added or modified without changing the front-end code.

This solution demonstrates dynamic UI rendering, database-driven form configuration, server-side validation, JWT authentication, clean architecture principles, and extensibility for future field types.

---

## Features

### Dynamic Form Rendering

* Form structure is generated from database configuration
* No UI code changes required when adding or modifying fields
* Supports:

  * TextBox
  * TextArea
  * Dropdown
  * Checkbox
* Supports future field types:

  * Radio Button
  * Date Picker
  * File Upload

### Configurable Field Properties

Each field supports:

* Label
* Field Name
* Input Type
* Placeholder
* Default Value
* Required Flag
* Display Order
* Column Span
* Validation Rules
* Dropdown Options

### Validation Support

Client-side Validation

* Required fields
* Submit button disabled until mandatory fields are completed
* Inline validation messages

Server-side Validation

* Required
* Numeric
* Max Length
* Regex
* Dropdown Option Validation

### Security

* JWT Authentication
* Protected APIs
* Swagger Authorization Support

### Form Submission

* Dynamic form data submitted to API
* Server-side validation performed
* Submission stored as JSON
* Proper HTTP status codes returned

---

## Architecture

### Solution Structure

```text
DynamicFormAssessment
│
├── DynamicForm.Api
├── DynamicForm.Application
├── DynamicForm.Domain
├── DynamicForm.Infrastructure
└── DynamicForm.UI
```

### Layer Responsibilities

#### Domain

Contains:

* Entities
* Business Models

#### Application

Contains:

* DTOs
* Interfaces
* Contracts

#### Infrastructure

Contains:

* EF Core DbContext
* Database Configuration
* Data Seeding

#### API

Contains:

* Controllers
* Authentication
* Swagger Configuration

#### UI

Contains:

* MVC Controllers
* Razor Views
* Dynamic Form Rendering

---

## Database Design

### Forms

| Column   | Type     |
| -------- | -------- |
| Id       | int      |
| FormKey  | nvarchar |
| Title    | nvarchar |
| Version  | int      |
| IsActive | bit      |

### FormFields

| Column       | Type     |
| ------------ | -------- |
| Id           | int      |
| FormId       | int      |
| FieldName    | nvarchar |
| Label        | nvarchar |
| InputType    | nvarchar |
| Placeholder  | nvarchar |
| DefaultValue | nvarchar |
| IsRequired   | bit      |
| DisplayOrder | int      |
| ColumnSpan   | int      |

### FieldValidations

| Column          | Type     |
| --------------- | -------- |
| Id              | int      |
| FormFieldId     | int      |
| ValidationType  | nvarchar |
| ValidationValue | nvarchar |
| ErrorMessage    | nvarchar |

### FieldOptions

| Column       | Type     |
| ------------ | -------- |
| Id           | int      |
| FormFieldId  | int      |
| OptionLabel  | nvarchar |
| OptionValue  | nvarchar |
| DisplayOrder | int      |

### FormSubmissions

| Column            | Type          |
| ----------------- | ------------- |
| Id                | int           |
| FormId            | int           |
| FormVersion       | int           |
| SubmittedDataJson | nvarchar(max) |
| SubmittedAt       | datetime      |

---

## Dynamic Form Configuration API

### Get Form Configuration

```http
GET /api/FormConfig/claim-details
```

Sample Response

```json
{
  "formKey": "claim-details",
  "title": "CLAIM DETAILS",
  "version": 1,
  "fields": [
    {
      "fieldName": "imageNo",
      "label": "Image No",
      "inputType": "textbox",
      "isRequired": true
    }
  ]
}
```

---

## Form Submission API

### Submit Form

```http
POST /api/FormSubmit
```

Request

```json
{
  "formKey": "claim-details",
  "formVersion": 1,
  "data": {
    "imageNo": "IMG-2024-0042",
    "taxId": "12345",
    "providerType": "Non PAR",
    "state": "Karnataka",
    "remarks": "Sample Remarks",
    "outcome": "Complete",
    "assignedTo": "rahul"
  }
}
```

Success Response

```json
{
  "success": true,
  "message": "Form submitted successfully."
}
```

Validation Failure Response

```json
{
  "success": false,
  "message": "Validation failed.",
  "errors": {
    "taxId": [
      "Tax ID is required"
    ]
  }
}
```

---

## Authentication

### Login Endpoint

```http
POST /api/Auth/login
```

Request

```json
{
  "userName": "admin",
  "password": "Admin@123"
}
```

Response

```json
{
  "token": "JWT_TOKEN",
  "expiresAt": "2026-01-01T12:00:00"
}
```

---

## Seeded Form

The application seeds a sample form named:

```text
CLAIM DETAILS
```

Fields included:

* Image No
* Tax ID
* Provider Type
* State
* Remarks
* Outcome
* Assigned To

---

## Technologies Used

Backend

* .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger

Frontend

* ASP.NET Core MVC
* Razor Views
* Bootstrap 5
* JavaScript

Tools

* Visual Studio 2022
* SQL Server
* Git
* GitHub

---

## Setup Instructions

### Clone Repository

```bash
git clone <repository-url>
```

### Update Connection String

Open:

```json
appsettings.json
```

Update:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=DynamicFormDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### Apply Migration

```bash
dotnet ef database update --project DynamicForm.Infrastructure --startup-project DynamicForm.Api
```

### Run API

```bash
dotnet run --project DynamicForm.Api
```

### Run MVC UI

```bash
dotnet run --project DynamicForm.UI
```

---

## Future Enhancements

* Redis Caching
* Docker Support
* xUnit Test Cases
* Role Based Authorization
* AutoMapper
* Repository Pattern
* Dynamic File Upload Controls
* Dynamic Date Picker Controls
* Multiple Form Versions
* Audit Logging
* Azure Deployment

---

## Author

Senior .NET Full Stack Developer

Built as part of a Dynamic Form Builder Assessment using .NET 8, SQL Server, Entity Framework Core, JWT Authentication, and ASP.NET MVC.
