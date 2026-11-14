# BudgetManager.Web

# BudgetManager

A personal finance management web application built with **ASP.NET Core MVC, .NET 10, Entity Framework Core, and SQL Server**.

BudgetManager is designed to help users manage recurring monthly bills, track payments, monitor vehicle charging expenses, and eventually interact with their financial data through an AI-powered assistant.

## Project Status

**Status:** Early development — database foundation completed.

The initial application architecture, entity models, database relationships, and SQL Server database have been established.

The next phase focuses on implementing the application services, user interfaces, and AI proof of concept.

## Technology Stack

- **Framework:** ASP.NET Core MVC (.NET 10)
- **Language:** C#
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core 10
- **Authentication:** ASP.NET Core Identity
- **Frontend:** Razor Views, HTML, CSS, JavaScript
- **Development Environment:** Visual Studio
- **AI Integration (Planned):** OpenAI API

## Features

### Implemented

**User Authentication Foundation**
- ASP.NET Core Identity configured with a custom `ApplicationUser` model
- User-specific relationships for bills and vehicles
- Identity database schema created in SQL Server

**Recurring Bill Data Model**
- Store recurring bill definitions
- Define expected amounts and monthly due days
- Support automatic payment indicators
- Track active and inactive bills
- Store individual monthly bill occurrences
- Record payments against bill occurrences

**Vehicle Charging Data Model**
- Associate vehicles with individual users
- Store vehicle information
- Record charging sessions
- Support Supercharger, home, third-party, and other charging types
- Track charging energy, price per kWh, and total cost

**Database**
- SQL Server configured as the EF Core provider
- Entity relationships and decimal precision configured
- Initial EF Core migration created and applied
- Database schema verified using SQL Server Management Studio

*Note: The implemented features above describe the current application and database foundation. User-facing CRUD screens and associated business services are still in development.*

### Planned

**Monthly Bill Management**
- Create, edit, and deactivate recurring bills
- Automatically generate monthly bill occurrences
- Record partial and full payments
- Calculate outstanding balances
- Display upcoming and overdue bills

**Vehicle Charging Management**
- Add and manage vehicles
- Record Tesla Supercharging sessions
- Track home and third-party charging costs
- Calculate monthly charging expenses
- Compare charging expenses across months

**Financial Dashboard**
- Monthly bill summary
- Total expected expenses
- Total paid and outstanding balances
- Upcoming payment reminders
- Charging expense summaries

**AI Budget Assistant**
- Conversational interface powered by a large language model
- Answer general budgeting questions
- Query authorized financial data through application services
- Summarize monthly expenses
- Explain spending patterns
- Analyze vehicle charging costs

The initial AI proof of concept will use the OpenAI API. Database-backed AI queries will be added in a later phase.

## Core Data Models

| Entity | Description |
|---|---|
| `ApplicationUser` | Application user extending ASP.NET Core Identity |
| `Bill` | Recurring monthly bill definition |
| `BillOccurrence` | Individual monthly instance of a bill |
| `BillPayment` | Payment recorded against a bill occurrence |
| `Vehicle` | User-owned vehicle |
| `ChargingSession` | Vehicle charging expense record |

### Entity Relationships

```text
ApplicationUser
├── Bills
│   └── BillOccurrences
│       └── BillPayments
└── Vehicles
    └── ChargingSessions
```

Each user's financial records are associated with their Identity user account. Application-level authorization and user-scoped data access will be enforced as the corresponding services and controllers are implemented.

## Getting Started

### Prerequisites

- .NET 10 SDK
- Visual Studio with ASP.NET and web development support, or a compatible .NET development environment
- Microsoft SQL Server
- SQL Server Management Studio (recommended)
- Git

### 1