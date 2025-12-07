# Kanban API

A RESTful API for a simple **Kanban board / task manager** application.  
Built with **C# ASP.NET Core**, **Entity Framework** and **MySQL**.  
Implements authentication, authorization, and role-based access control.

---

## Features

-   **Authentication** with JWT (login/register)
-   **Role-based authorization**:
    -   `USER`: create/edit their own tickets
    -   `MANAGER`: manage tickets, view users
    -   `ADMIN`: full access (users + tickets)
-   **Tickets** with status, priority, and user assignments
-   **Users** with roles and status
-   **Secure password hashing**
-   **JPA Repositories** for clean ORM-based persistence

---

## Tech Stack

-   ASP.NET Core
-   Entity Framework - database modeling & queries
-   MySQL - relational database
-   JWT - authentication

---

## Database Schema

Using **Entity Framework + MySQL**:

### `users`

-   `id` (PK, auto-increment)
-   `username` (unique)
-   `password` (hashed)
-   `role` (`USER`, `MANAGER`, `ADMIN`)
-   `email` (unique, optional)
-   `status` (`ACTIVE`, `INACTIVE`)
-   timestamps

### `tickets`

-   `id` (PK, auto-increment)
-   `title` (varchar 255)
-   `details` (text, optional)
-   `status` (`TO_DO`, `IN_PROGRESS`, `IN_REVIEW`, `DONE`)
-   `priority` (`LOW`, `MEDIUM`, `HIGH`)
-   `created_by` (FK -> users.id)
-   `assigned_to` (FK -> users.id, optional)
-   timestamps

---

## API Routes

### 🔑 Auth

`/api/auth`

-   `POST /register` -> Register a new user
    -   example body:
    ```json
    {
        "username": "username",
        "password": "password",
        "email": "email@example.com" // (optional)
    }
    ```
-   `POST /login` -> Authenticate and return JWT
    -   example body:
    ```json
    {
        "username": "username",
        "password": "password"
    }
    ```

### 👤 Users

`/api/users`

-   `GET /profile` -> Current user info (requires auth)
-   `POST /` -> Create user (ADMIN only)
    -   example body:
    ```json
    {
        "username": "username",
        "password": "password",
        "role": "ADMIN",
        "email": "email@example.com" // (optional)
    }
    ```
-   `GET /:id` -> Get user by id (MANAGER/ADMIN)
-   `GET` -> Get all users (MANAGER/ADMIN)
-   `PATCH /:id` -> Update user (ADMIN only)
    -   example body (each field is optional):
    ```json
    {
        "username": "username",
        "password": "password",
        "role": "ADMIN",
        "email": "email@example.com",
        "status": "ACTIVE"
    }
    ```
-   `DELETE /:id` -> Delete user (ADMIN only)

### 🎫 Tickets

`/api/tickets`

-   `POST /` -> Create a new ticket (any authenticated user)
    -   example body:
    ```json
    {
        "title": "A title",
        "details": "A description"
    }
    ```
-   `GET /:id` -> Get ticket by id (auth required)
-   `GET /` -> Get all tickets (auth required)
-   `PATCH /:id` -> Update a ticket (auth required, role-sensitive)
    -   example body (each field is optional):
    ```json
    {
        "assigned_to": 1,
        "title": "A title",
        "details": "A description",
        "status": "IN_PROGRESS", // only managers and admins can set to "DONE"
        "priority": "MEDIUM"
    }
    ```
-   `DELETE /:id` -> Delete a ticket (MANAGER/ADMIN)

---

## Setup & Run

Clone the repository:

```bash
git clone https://github.com/Sasho1256/kanban-net.git
cd kanban-api
```

### Local Development

#### 1. Environment variables

Copy appsettings.json as appsettings.Development.json:

```bash
cd kanban-net
cp appsettings.json appsettings.Development.json
```

Replace the db password and jwt secret placeholders with your actual secrets:

```json
"ConnectionStrings": {
    "Default": "Server=localhost;Database=kanban_api;User=root;Password=YOUR_PASSWORD;TreatTinyAsBoolean=false;"
},
"Jwt": {
    "Key": "YOUR_SECRET"
}
```

#### 2. Install dependencies

```bash
dotnet build
```

#### 3. Run DB migrations

```bash
dotnet ef database update
```

#### 4. Run the application

```bash
dotnet run
```

## Future Improvements

-   Error-specific responses
-   Extensive unit testing
-   Rate limits
-   Ticket comments
-   User profile settings
-   Pagination & filtering for tickets
-   Dockerization
-   Front-End integration
-   Deployment
