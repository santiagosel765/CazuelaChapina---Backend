# 🥘 CazuelaChapina - Backend (.NET)

## ✨ Overview
CazuelaChapina is a modular backend built with **.NET 8** that powers a sales system for traditional Guatemalan tamales and beverages.  
It supports multiple branches, seasonal combos, inventory control, offline synchronization, push notifications and an analytics dashboard to keep the business running smoothly.

## 🚀 Technologies
| Technology | Why |
| --- | --- |
| **.NET 8 & ASP.NET Core Web API** | High‑performance, cross‑platform backend framework |
| **Entity Framework Core (PostgreSQL)** | Productive ORM with LINQ support and migrations |
| **JWT Authentication** | Stateless security for APIs |
| **AutoMapper** | Simplifies mapping between entities and DTOs |
| **FluentValidation** | Consistent request validation |
| **Firebase Cloud Messaging** | Push notifications to devices |
| **OpenRouter API** | LLM‑powered FAQ endpoint |
| **Swagger / OpenAPI** | Interactive API documentation |
| **xUnit** | Unit testing framework |

## 🔐 Security
- **JWT** tokens authenticate users.  
- **Roles & permissions** are enforced via custom `PermissionAuthorize` middleware.  
- Endpoints declare required permissions and reject unauthorized requests.  
- HTTPS is recommended in production to protect tokens in transit.

## 🧱 Project Structure
```
CazuelaChapina.API/
├── Controllers/         # HTTP endpoints
├── DTOs/                # Request & response models
├── Models/              # Entity Framework entities
├── Services/            # Business logic
├── Repositories/        # Data access layer
├── Mappings/            # AutoMapper profiles
├── Validators/          # FluentValidation rules
├── Middleware/          # JWT & permission filters
├── Data/                # DbContext and migrations
└── ...                  # Config files
CazuelaChapina.Tests/    # xUnit tests
```

## 🔗 Core Endpoints
### Authentication & Users
- `POST /api/auth/login` – issue JWT
- `POST /api/auth/register` – create user
- `POST /api/users/{id}/roles` – assign role
- `POST /api/roles/{id}/permissions` – set role permissions

### Product Catalog
- **Tamales** – `GET/POST/PUT/DELETE /api/tamales`
- **Beverages** – `GET/POST/PUT/DELETE /api/beverages`
- **Combos** – `GET/POST/PUT/DELETE /api/combos`, `POST /api/combos/{id}/clone`, `POST /api/combos/{id}/activate`, `POST /api/combos/{id}/deactivate`

### Inventory
- `GET/POST/PUT/DELETE /api/inventory`
- `POST /api/inventory/{id}/entry`
- `POST /api/inventory/{id}/exit`
- `POST /api/inventory/{id}/waste`

### Sales & Sync
- `GET /api/sales`
- `GET /api/sales/{id}`
- `POST /api/sales`
- `POST /api/sync/sale` – sync offline sale

### Branches
- `GET /api/branches`
- `POST /api/branches`
- `POST /api/branches/{branchId}/assign/{userId}`
- `GET /api/branches/{id}/report`

### Dashboard & FAQ
- `GET /api/dashboard/summary`
- `GET /api/faq?question=...`

### Notifications
- `POST /api/notifications/register`
- `POST /api/notifications/cooking-complete`

## 🧮 Implemented Modules
- **Product Catalog** – tamales, beverages and seasonal combos
- **Seasonal Combo Management** – clone, activate and deactivate combos
- **Inventory & Waste** – track raw materials, stock movements and mermas
- **Sales Registry** – record sales, adjust stock and notify devices
- **Analytical Dashboard** – consolidated metrics for decision making
- **FAQ with OpenRouter** – AI‑powered responses to common questions
- **Branches & Offline Sales** – branch management and offline synchronization
- **Push Notifications** – Firebase Cloud Messaging integration

## ⚙️ Local Setup
### Requirements
- .NET 8 SDK
- PostgreSQL 14+

### Environment Variables
Configure the following (or edit `appsettings.json`):
```
ConnectionStrings__DefaultConnection="Host=localhost;Port=5432;Database=cazuela_db;Username=postgres;Password=root"
Jwt__Key="SUPER_SECRET_KEY_CHANGE_ME"
OpenRouter__ApiKey="<your_openrouter_key>"
Fcm__ServerKey="<your_fcm_key>"
```

### Run
```bash
dotnet restore
dotnet ef database update --project CazuelaChapina.API
dotnet run --project CazuelaChapina.API
```
Swagger UI: `https://localhost:5001/swagger`

## 🧪 Manual Testing
1. Start the API.
2. Open Swagger or Postman.
3. Obtain a JWT via `POST /api/auth/login`.
4. Include `Authorization: Bearer <token>` header for protected endpoints.

## 📊 Frontend Indicators
`GET /api/dashboard/summary` exposes metrics such as:
- Daily & monthly sales
- Top 3 tamales
- Most popular beverages (morning/afternoon/night)
- Sales by spice level
- Profit by tamales, beverages and combos
- Waste quantity and cost

## 📌 Roadmap
- Telemetry and centralized logging
- Dining room table layout management
- Supplier & purchase order tracking
- Multi-language support

## 📄 License
Released under the **MIT License**.
