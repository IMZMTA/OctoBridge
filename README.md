# 🚀 OctoBridge - GitHub Connector API

A modular **ASP.NET Core (.NET 10) Web API** that integrates with GitHub using:

* 🔐 OAuth 2.0 (Full-featured mode)
* 🔑 Personal Access Token (PAT) (Quick testing mode)

---

# 📌 Overview

This project demonstrates:

* External API integration (GitHub REST API)
* Secure authentication (OAuth 2.0 + JWT + Cookies)
* Clean Architecture with CQRS
* Production-ready practices (Encryption, Validation, Error Handling)

---

# ✨ Key Features

* GitHub OAuth Login
* PAT-based access (server + user)
* Fetch repositories
* List & create issues
* Create pull requests
* Fetch commits
* JWT Authentication (HttpOnly Cookie)
* AES Encryption for tokens
* CQRS with MediatR
* FluentValidation
* Swagger (NSwag)

---

# 🏗️ Tech Stack

| Layer        | Technology                |
| ------------ | ------------------------- |
| Backend      | ASP.NET Core (.NET 10)    |
| Architecture | Clean Architecture + CQRS |
| ORM          | Entity Framework Core     |
| Database     | SQLite                    |
| Auth         | JWT + OAuth 2.0           |
| Validation   | FluentValidation          |
| Docs         | Swagger / NSwag           |

---

# 📂 Project Structure

```
OctoBridge.API           → Controllers, Middleware
OctoBridge.Application   → CQRS, Handlers, Validators
OctoBridge.Domain        → Entities, Configs
OctoBridge.Infrastructure→ Services, DB, Encryption
```

---

# ⚙️ Setup Instructions

```bash
git clone <repo-url>
cd OctoBridge
dotnet restore
dotnet ef database update --project OctoBridge.Infrastructure --startup-project OctoBridge.API
dotnet run --project OctoBridge.API
```

Swagger:

```
http://localhost:5182/swagger
```

---

# ⚙️ Configuration (appsettings.json)

```json
{
  "Jwt": {
    "Key": "MIN_32_CHAR_SECRET",
    "Issuer": "OctoBridgeAPI",
    "Audience": "OctoBridgeClient"
  },
  "Encryption": {
    "Key": "32_BYTE_KEY",
    "IV": "16_BYTE_IV"
  },
  "OAuth": {
    "ClientId": "...",
    "ClientSecret": "...",
    "RedirectUri": "http://localhost:5182/api/v1/auth/github/callback",
    "Scope": "read:user user:email repo"
  }
}
```

---

# 🔐 Authentication Modes

## 1️⃣ OAuth 2.0 (Full Mode - Recommended)

Supports full GitHub functionality:

* Repositories
* Issues
* Pull Requests
* Commits

### Flow:

```
Login → GitHub → Callback → Token Stored → JWT Issued → API Access
```

---

## 2️⃣ Personal Access Token (PAT)

Used for quick testing:

| Endpoint                     | Description            |
| ---------------------------- | ---------------------- |
| GET `/api/v1/pat/via-server` | Uses server PAT        |
| POST `/api/v1/pat/via-users` | Uses user-provided PAT |

⚠️ Limited functionality (profile-level access)

---

# 🔌 API Endpoints

## 🔐 Auth

| Endpoint                           | Description    |
| ---------------------------------- | -------------- |
| `/api/v1/auth/login`               | Start OAuth    |
| `/api/v1/auth/{provider}/callback` | OAuth callback |
| `/api/v1/auth/logout`              | Logout         |
| `/api/v1/auth/disconnect`          | Disconnect     |

---

## 🐙 GitHub (OAuth Required)

| Method | Endpoint                       | Description  |
| ------ | ------------------------------ | ------------ |
| GET    | `/github/repositories`         | Get repos    |
| GET    | `/github/list-issues`          | List issues  |
| POST   | `/github/create-issues`        | Create issue |
| POST   | `/github/create-pull-requests` | Create PR    |
| GET    | `/github/get-commits`          | Get commits  |

---

# 🔄 System Flow

```
Client → Controller → CQRS → Service → GitHub API → Response
```

---

# 🔁 Internal Processing

1. JWT extracted from cookie
2. User fetched from DB
3. GitHub token decrypted
4. API call executed
5. Response returned

---

# 🧱 Architecture

* Clean Architecture
* CQRS Pattern
* Dependency Injection
* Middleware-based error handling

---

# 🗄️ Database

**Users Table**

* Id
* GitHubId
* Username
* Email
* Provider
* EncryptedToken

---

# 🔐 Security Practices

* AES encryption for tokens
* JWT in HttpOnly cookies
* No hardcoded secrets
* Input validation
* Centralized exception handling

---

# ❗ Common Issues

* OAuth fails → Check redirect URI
* UserId = 0 → Missing JWT cookie
* GitHub API fails → Check scopes

---

# 📌 Conclusion

A **production-ready GitHub Connector API** demonstrating:

* Secure authentication
* Clean architecture
* Scalable backend design

---
