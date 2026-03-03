<div align="center">

# 💼 Finance Management System

**Enterprise-grade personal & corporate finance tracking API built with Clean Architecture**

[![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![Clean Architecture](https://img.shields.io/badge/Clean_Architecture-FF6B6B?style=for-the-badge&logo=buffer&logoColor=white)]()

</div>

---

## 📖 Overview

Finance Management System is a RESTful API designed to manage income, expenses, budgets, and financial reports for individuals or teams. Built following **Clean Architecture** principles with strict separation of concerns.

## 🏗️ Architecture

```
├── WebApi/                    # Controllers, middleware, startup
├── Application/               # Use cases, DTOs, interfaces
├── Core.Application/          # Shared application abstractions
├── Domain/                    # Entities, business rules
├── Persistence/               # EF Core repositories, migrations
├── Core.Persistence/          # Generic repository base
├── Infrastructure/            # External service integrations
└── Core.CrossCuttingConcerns/ # Logging, caching, validation
```

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Architecture | Clean Architecture + CQRS |
| Validation | FluentValidation |
| Documentation | Swagger / OpenAPI |
| Database | PostgreSQL / MSSQL |

## ✨ Features

- 📊 Income & expense tracking
- 💰 Budget planning & reporting
- 🔍 Category-based filtering
- 🔐 JWT authentication
- 📈 Financial summary endpoints
- 🧱 Generic repository pattern

## �� Getting Started

```bash
git clone https://github.com/enesbilik/finance-management-system.git
cd finance-management-system

# Restore & run
dotnet restore
dotnet run --project WebApi
```

API available at `http://localhost:5000` — Swagger at `/swagger`

## 📄 License

MIT
