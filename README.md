# MediFlow — Healthcare SaaS Starter

Full-stack portfolio application with React, ASP.NET Core 8 Web API, EF Core and Microsoft SQL Server. Supports clinic registration, JWT authentication, clinic-scoped patient/doctor/appointment CRUD, role checks and a responsive dashboard.

> **Portfolio / educational starter, NOT production-ready for real patient data.** Use synthetic data only. This repository does not claim HIPAA, GDPR or other healthcare regulatory compliance. Production requires security review, privacy impact assessment, audit logs, MFA, encryption and key management, backup/recovery, monitoring, access governance, appropriate consent, and more.

## Requirements
- .NET 8 SDK
- SQL Server 2019+ or SQL Server Express / LocalDB
- Node.js 20+
- EF Core CLI (`dotnet tool install --global dotnet-ef --version 8.0.11`)

## Start backend

```powershell
cd backend/MediFlow.Api
dotnet restore
dotnet user-secrets init
dotnet user-secrets set "Jwt:Key" "YOUR_RANDOM_SECRET_WITH_AT_LEAST_32_CHARACTERS"
# Change the SQL Server connection string if your local instance differs:
# dotnet user-secrets set "ConnectionStrings:Default" "Server=localhost;Database=MediFlowDb;Trusted_Connection=True;TrustServerCertificate=True;"
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run --urls http://localhost:5000
```

Open Swagger at `http://localhost:5000/swagger` (set `ASPNETCORE_ENVIRONMENT=Development` if needed). `dotnet ef migrations add InitialCreate` generates the initial EF migration from the included models; migration files are intentionally not pre-generated.

## Start frontend

```powershell
cd frontend
npm install
copy .env.example .env
npm run dev
```

Open `http://localhost:5173`, choose **Create clinic**, and register an initial clinic administrator. Then add doctors, patients and appointments. The app starts with an empty database (no fake production data).

## Architecture

- `frontend/src/App.jsx` — React UI and route-based pages
- `frontend/src/lib/api.js` — authenticated API requests
- `backend/MediFlow.Api/Controllers/AuthController.cs` — clinic signup and login
- `backend/MediFlow.Api/Controllers/RecordsController.cs` — clinic-scoped REST endpoints
- `backend/MediFlow.Api/Data/AppDbContext.cs` — EF Core SQL Server context
- `backend/MediFlow.Api/Models/Entities.cs` — domain models

## API routes

- `POST /api/auth/register`, `POST /api/auth/login`
- `GET /api/dashboard`
- `GET, POST /api/patients`; `PUT, DELETE /api/patients/{id}`
- `GET, POST /api/doctors`; `PUT, DELETE /api/doctors/{id}`
- `GET, POST /api/appointments`; `PUT, DELETE /api/appointments/{id}`

JWT claims contain a clinic ID; all record reads and writes are scoped to that clinic. The initial registered user is an Admin. Roles Doctor and Receptionist are defined in the API, but user invitations / role assignment UI are not yet implemented. This is a foundation, not a fully featured healthcare product.

## Security limitations and next steps

- Add per-doctor assignment checks; currently Doctor role can edit any appointment within the clinic.
- Add pagination, server-side validation, concurrency checks, appointment overlap detection, refresh tokens, account lockout, MFA and password reset.
- Implement an audited staff invitation flow and tenant administration.
- Add audit trails, least-privilege policies, secret rotation, secure deployment and healthcare compliance controls before real patient data.
- Improve error messages in the frontend (currently some raw API error text is shown).
- Add automated tests and CI, and verify production configuration.

## GitHub description

> MediFlow is a multi-clinic healthcare management SaaS starter built with React, ASP.NET Core Web API, EF Core and SQL Server, featuring JWT authentication, role-based API access, patient and doctor management, appointment scheduling, and a responsive analytics dashboard.

MIT licensed (see LICENSE).
