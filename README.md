####

# 🚀 OctoBridge - GitHub Connector API

A clean, modular **.NET 10 Web API** that integrates with GitHub using:

* 🔐 OAuth 2.0
* 🔑 Personal Access Tokens (PAT)

This project demonstrates **real-world backend architecture, authentication, and secure API integration**.

---

# 📖 Context

This project is designed to understand:

* External API integration (GitHub REST API)
* Authentication flows (OAuth 2.0 + JWT + Cookies)
* Clean architecture (CQRS + Separation of Concerns)
* Production-ready practices (AES Encryption, error handling, configuration)

---

# 📑 Index

01. Features
02. Tech Stack
03. Project Structure
04. How to Get the Project
05. Setup Instructions
06. Configuration (appsettings.json)
07. Authentication Methods
08. GitHub OAuth Setup
09. GitHub PAT Setup
10. API Endpoints
11. System Flow
12. Internal Processing Flow
13. Architecture Breakdown
14. ER Diagram
15. Security Practices
16. Common Issues

---

# 📌 01. Features

✅ GitHub OAuth Login
✅ Personal Access Token (PAT) support
✅ Fetch repositories
✅ Fetch commits
✅ List issues
✅ Create issues
✅ Create pull requests (bonus)
✅ JWT Authentication (stored in HttpOnly Cookie)
✅ AES Encryption for sensitive data
✅ CQRS pattern with MediatR
✅ FluentValidation for input validation
✅ Swagger (NSwag) integration

---

# 🏗️ 02. Tech Stack

| Layer        | Technology                |
| ------------ | ------------------------- |
| Backend      | .NET 10 (ASP.NET Core)    |
| Architecture | Clean Architecture + CQRS |
| ORM          | Entity Framework Core     |
| Database     | SQLite                    |
| Auth         | JWT + OAuth 2.0           |
| Validation   | FluentValidation          |
| API Docs     | Swagger / NSwag           |
| Resilience   | Polly                     |

---

# 📂 03. Project Structure

OctoBridge.API           → Entry point (Controllers, Middleware, Swagger)
OctoBridge.Application   → CQRS Handlers, Validators, DTOs
OctoBridge.Infrastructure→ Services (GitHub, Auth, DB, Encryption)
OctoBridge.Domain        → Entities, Enums, Constants

---

# 📥 04. How to Get This Project SetUp and Run

# 🛠️ Setup Instructions

## Clone from Git(Repositories)

```bash
git clone <repo-url>
cd OctoBridge
```

---

## Restore Dependencies

```bash
dotnet restore
```

---

# 🛠️ 05. Setup Instructions

## Step 1: Configure appsettings.json

(See full configuration section below)

---

## Step 2: Apply Database Migration

```bash
dotnet ef database update --project OctoBridge.Infrastructure --startup-project OctoBridge.API
```

---

## Step 3: Run Application

```bash
dotnet run --project OctoBridge.API
```

---

## Step 4: Access API

* App → http://localhost:5182
* Swagger → http://localhost:5182/swagger( ## Recommended )

---

# ⚙️ 06. Configuration (appsettings.json)

## 🔑 JWT

```json
"Jwt": {
  "Key": "MINIMUM_32_CHAR_SECRET",
  "Issuer": "OctoBridgeAPI",
  "Audience": "OctoBridgeClient"
}
```

---

## 🔐 Encryption (AES)

```json
"Encryption": {
  "Key": "EXACT_32_BYTE_KEY",
  "IV": "EXACT_16_BYTE_IV"
}
```

---

## 🐙 GitHub OAuth

```json
"OAuth": {
  "ClientId": "YOUR_CLIENT_ID",
  "ClientSecret": "YOUR_CLIENT_SECRET",
  "RedirectUri": "http://localhost:5182/api/v1/auth/github/callback",
  "Scope": "read:user user:email repo"
}
```

---

## 🗄️ Database

```json
"DefaultConnection": "Data Source=../SQLiteDatabase/OctoBridgeDatabase.db"
```

---

## 🔑 Optional: Server PAT

```json
"GitHubPAT": "your_token_here"
```

---

# 🔐 07. Authentication Methods

## 1️⃣ OAuth (Recommended)

### Flow:

1. Call:

```
GET /api/v1/auth/login?provider=GitHub
```

2. Redirect to GitHub
3. User authorizes
4. GitHub redirects back

```
/api/v1/auth/github/callback
```

5. Backend:

* Exchange code → access token
* Fetch user info
* Store token (AES encrypted in DB)
* Generate JWT
* Store JWT in **HttpOnly Cookie**

✅ After this → All APIs work automatically

---

## 2️⃣ Personal Access Token (PAT)

### a) Server PAT

```
GET /api/v1/pat/via-server
```

---

### b) User PAT

```
POST /api/v1/pat/via-users
```

```json
{
  "personalAccessToken": "ghp_xxx"
}
```

⚠️ Not recommended for production

---



# 🔑 08. GitHub OAuth Setup

Follow these steps to enable GitHub OAuth login.

---


### 1️⃣ Go to GitHub Developer Settings

![Image](https://www.freecodecamp.org/news/content/images/2022/10/image-230.png)

![Image](https://user-images.githubusercontent.com/25517624/159352346-35a647d3-6864-4cdb-bc52-7cb7203e3911.png)

![Image](https://user-images.githubusercontent.com/3988879/40750372-4c5ae452-6424-11e8-8eda-67d03ca548fc.png)

![Image](https://www.freecodecamp.org/news/content/images/2022/10/image-232.png)

* Open: https://github.com/settings/developers


1. Open https://github.com/settings/developers
2. Click OAuth Apps → New OAuth App


### 2️⃣ Fill OAuth App Details

![Image](https://access.redhat.com/webassets/avalon/d/Red_Hat_Quay-3.2-Use_Red_Hat_Quay-en-US/images/47b2c8021cfe3030742acbb7120cc30c/register-app.png)

![Image](https://access.redhat.com/webassets/avalon/d/Red_Hat_Quay-3.4-Use_Red_Hat_Quay-en-US/images/47b2c8021cfe3030742acbb7120cc30c/register-app.png)

![Image](https://authjs.dev/_next/image?q=75\&url=%2F_next%2Fstatic%2Fmedia%2Fcallback-url.e3627403.webp\&w=3840)

![Image](https://images.ctfassets.net/hcqpbvoqhwhm/67kFoKTMRZy9DM0PT1D0ie/23e4dbd3fd16ebcceb98c90c188d58d7/github-register-new-oauth-filled.png)



Fill the form:

| Field                      | Value                                             |
| -------------------------- | ------------------------------------------------- |
| Application Name           | OctoBridge                                        |
| Homepage URL               | http://localhost:5182                             |
| Authorization Callback URL | http://localhost:5182/api/v1/auth/github/callback |

3. Click **Register Application**

---

### 3️⃣ Copy Credentials

![Image](https://episyche-blog.s3.ap-south-1.amazonaws.com/NextJS/Github/94/content_image_text/72be5657-3163-4869-a16b-77d6081a864b.png)

![Image](https://episyche-blog.s3.ap-south-1.amazonaws.com/NextJS/Github/94/content_image_text/99535f64-19d3-4558-9ac7-9c2ee247fdab.png)

![Image](https://tinyauth.app/screenshots/github/oauth-secret.png)

![Image](https://authjs.dev/_next/image?q=75\&url=%2F_next%2Fstatic%2Fmedia%2Fclientid-secret.73de72fd.webp\&w=3840)

* Copy:

  * `Client ID`
  * `Client Secret` (Generate if not visible)

---

4. Update appsettings.json

```json
"OAuth": {
  "BaseUrl": "https://github.com/",
  "AuthorizationUrl": "login/oauth/authorize",
  "TokenUrl": "login/oauth/access_token",
  "ClientId": "YOUR_CLIENT_ID",
  "ClientSecret": "YOUR_CLIENT_SECRET",
  "RedirectUri": "http://localhost:5182/api/v1/auth/github/callback",
  "Scope": "read:user user:email repo"
}
```

---

### 5️⃣ Run the Application

```bash
dotnet run --project OctoBridge.API
```

Open Swagger:

```
http://localhost:5182/swagger
```

---

---

### 6️⃣ Start OAuth Login

Call:

```
GET /api/v1/auth/login?provider=GitHub
```

➡️ This will:

1. Redirect to GitHub
2. Ask user permission
3. Redirect back to your API
4. Log the user in

---

### ✅ Success Result

* JWT stored in **HttpOnly cookie**
* User authenticated
* GitHub APIs ready to use

---

### ⚠️ Common Mistakes

* ❌ Wrong Redirect URI
* ❌ Missing `repo` scope
* ❌ Client Secret not set
* ❌ App not running on same port


# 🔑 09. GitHub PAT Setup

1. Open https://github.com/settings/tokens

2. Generate Token

### Recommended: Fine-Grained

Permissions:

* Issues → Read & Write
* Pull Requests → Read & Write
* Contents → Read

---

3. Copy token (only shown once)

---

4. Use in:

* appsettings.json (server PAT) 
* API request (user PAT) (Not Recommended)

---

# 🔌 10. API Endpoints

## 🔐 Auth

| Endpoint                           | Description       |
| ---------------------------------- | ----------------- |
| `/api/v1/auth/login`               | Start OAuth login |
| `/api/v1/auth/{provider}/callback` | OAuth callback    |
| `/api/v1/auth/logout`              | Logout user       |
| `/api/v1/auth/disconnect`          | Disconnect GitHub |

---

## 🐙 GitHub

### To get all Repositories

GET /api/v1/github/repositories

### Issues

GET /api/v1/github/list-issues
POST /api/v1/github/create-issues

### Pull Requests

POST /api/v1/github/create-pull-requests

### Commits

GET /api/v1/github/get-commits

---

## 🔑 PAT APIs

GET /api/v1/pat/via-server
POST /api/v1/pat/via-users

---

# 🔄 11. System Flow

Client → Controller → CQRS → Service → GitHub API → Response

---

# 🔁 12.a Internal Flow

1. User authenticates (OAuth or PAT)
2. Access token stored securely (AES encrypted)
3. JWT generated and stored in cookie
4. On each request:

   * JWT → Extract userId & providerId
   * Fetch user from DB
   * Decrypt GitHub token
   * Call GitHub API via service layer

# 🔄 12.b Internal Processing Flow

1. Request hits Controller
2. Sent to CQRS Handler
3. Validator checks input
4. JWT extracted
5. User fetched from DB
6. Token decrypted
7. GitHub API called
8. Response mapped
9. Returned

# 🛡️ Security Practices

✅ No hardcoded secrets
✅ AES encryption for tokens
✅ JWT in HttpOnly cookie
✅ Input validation (FluentValidation)
✅ Centralized error handling

---

# 🧱 13. Architecture Breakdown

API Layer → Controllers, Middleware
Application Layer → CQRS, Validators
Domain Layer → Entities, Model, Constant, Config
Infrastructure Layer → Services, DB, Encryption, Client

## 🧭 Architecture Overview

Below is the high-level architecture of the system:

---

![Image](https://miro.medium.com/v2/resize%3Afit%3A1400/1%2ARjbGaDvjFu2mg1UlJTKYog.png)

![Image](https://media.licdn.com/dms/image/v2/D4D12AQFqyLoq79KY6A/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1687467593745?e=2147483647\&t=5lrGfIcXAu2ivE58cqA9f3tSRJvcvEMTKvKruhA84YM\&v=beta)

![Image](https://ronaldbosma.github.io/images/apim-oauth-series/call-oauth-protected-apis-from-github-actions-using-federated-credentials/diagrams-overview-github-actions.png)

![Image](https://capgemini.github.io/images/2018-07-13-combining-oauth-and-jwt-to-gain-performance-improvements/authentication-flow.jpg)

---

### 🔁 Request Flow

```
Client (Swagger / Frontend)
        ↓
Controllers (API Layer)
        ↓
CQRS Handler (Application Layer)
        ↓
Services (Infrastructure Layer)
        ↓
GitHub API
        ↓
Response → Back to Client
```

---

### 🧱 Layers Explained

#### 🟦 API Layer (OctoBridge.API)

* Controllers
* Middleware
* Swagger (NSwag)
* Handles HTTP requests/responses

---

#### 🟩 Application Layer (OctoBridge.Application)

* CQRS (Commands & Queries)
* Handlers
* Validators (FluentValidation)
* DTOs

---

#### 🟨 Domain Layer (OctoBridge.Domain)

* Entities
* Enums
* Constants
* Core business rules

---

#### 🟥 Infrastructure Layer (OctoBridge.Infrastructure)

* GitHub Service (API calls)
* User Service (DB access)
* Encryption Service (AES)
* User Context (JWT extraction)

---

### 🔐 Authentication Flow

```
User → GitHub OAuth → Callback
        ↓
Access Token received
        ↓
Encrypted & stored in DB
        ↓
JWT generated
        ↓
Stored in HttpOnly Cookie
        ↓
Used in every request
```

--

---

# 🗄️ 14. ER Diagram

Users Table:

* Id
* GitHubId
* Username
* Email
* Provider
* EncryptedToken
* CreatedAt

Optional Activities Table:

* Id
* UserId
* ActionType
* Metadata
* CreatedAt

Relationship:

* One User → Many Activities

---

# 🛡️ 15. Security Practices

✅ AES encryption for tokens
✅ JWT stored in HttpOnly cookie
✅ No hardcoded secrets
✅ Input validation
✅ Centralized error handling

---

# ❗16. Common Issues

### UserId = 0

* JWT missing
* Cookie not sent
* OAuth not completed

---

### OAuth Fails

* Wrong ClientId / Secret
* Incorrect Redirect URI
* Port mismatch

---


# 📎 Conclusion

A **production-ready GitHub Connector API** demonstrating:

* Secure authentication
* Clean architecture
* Scalable backend design

Perfect for learning and real-world use 🚀

---------------------------------------------