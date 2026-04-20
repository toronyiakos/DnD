# Realm Codex – D&D 5e Backend

ASP.NET Core Web API backend for a Dungeons & Dragons 5th Edition online platform.

## Tech Stack

- **ASP.NET Core 8 Web API** – RESTful backend framework
- **Entity Framework Core** – ORM for database access
- **JWT (JSON Web Token)** – Authentication & authorization
- **Swagger / OpenAPI** – API documentation
- **Rate Limiting** – Request throttling and abuse protection

## Quick Start

```bash
# Restore dependencies
dotnet restore

# Configure secrets (Development)
dotnet user-secrets init

dotnet user-secrets set "ConnectionStrings:Default" "server=;database=;user=;password="
dotnet user-secrets set "Jwt:Secret" "your-super-secret-key"

# Run the application
dotnet run
```

API URL: `http://localhost:5000`  
Swagger UI: `http://localhost:5000/swagger`

### Production Environment Variables
ConnectionStrings__Default=...  
Jwt__Secret=...

## Project Structure

```
Backend/
├── ConfigModels/      # Api option models
├── Controllers/      # HTTP endpoints
├── DTO/             # Data transfer objects
├── Helpers/             # Extensions
├── Models/           # Entity models (EF Core) & DbContext
├── Resources/           # Localization files
├── Services/         # Business logic
└── Program.cs        # Application bootstrap
```

---

## Features

### Authentication
- JWT-based authentication
- Secure token generation with expiration
- Role-based authorization (user / game_master / admin)
- Protected endpoints via [Authorize]

### Users & Roles
- User registration and login
- Role management (admin only)
- Claims-based identity handling

### Characters (/characters)
- Full CRUD operations
- Relations: class, race, background, alignment
- Ability score management
- Inventory and spell associations

### Spells (/spells)
- Filterable spell queries
- CRUD operations (admin / GM)
- Class-based spell linking

### Battlemap (/map)
- Token-based map system
- Player / NPC / monster separation
- Position tracking and updates

### Admin (/admin)
- User management
- Role updates
- User deletion

---

## Security

### Authentication & Authorization
- JWT Bearer authentication
- Role-based access control (RBAC)

### API Protection
- Rate limiting (global + login endpoints)
- Centralized exception handling

### Security Headers
- Content-Security-Policy
- X-Frame-Options
- X-Content-Type-Options
- Referrer-Policy

### Injection Protection
- ORM-based queries (EF Core)
- No raw SQL usage
- Parameterized queries by default

---

## API Documentation

Swagger (OpenAPI) is enabled in development mode.

### Features
- Endpoint listing
- Request/response schemas
- Authentication testing (Bearer token)
- Example payloads

---

## Environment Variables

ConnectionStrings__Default – Database connection string  
JWT_SECRET – Secret key for token signing  
ASPNETCORE_ENVIRONMENT – Environment (Development / Production)

---

## Configuration

Configuration is loaded from:
- appsettings.json
- Environment variables
- User Secrets (development)

Sensitive data (passwords, tokens, connection strings) are not stored in source code.

---

## Error Handling

- Global exception middleware
- Consistent JSON error responses
- Proper HTTP status codes

### Example
```
{
  "message": "internal_server_error"
}
```

---

## Rate Limiting

- Global limiter (per user/IP)
- Login endpoint protection

### Example limits
- 5 requests / minute for login
- 60 requests / minute global

---

## Localization

- Supports multi-language responses (English, Hungarian)
- Based on Accept-Language header
- Implemented using IStringLocalizer

### Example messages
- Invalid credentials
- Username already taken
- Character not found

---

## Backend–Frontend Integration

The backend serves a REST API consumed by the React frontend.

### Communication
- JSON over HTTP
- JWT for authentication
- CORS enabled for frontend origin
