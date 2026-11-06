# Game Store API

A lightweight, modern RESTful backend API for managing a video game catalog and store, built with **ASP.NET Core Minimal APIs** and **Entity Framework Core**.

---

# There is another version of this app with the frontend using Angular. Check it out here [GameProject_FullStack](https://github.com/shadowofdominance/GameProject_FullStack.git).

---

## 📌 Project Overview

**Game Store API** is an ASP.NET Core web service designed to manage video game listings and genres. Built with a clean, decoupled architecture, it showcases best practices using Minimal APIs, Data Transfer Objects (DTOs), Data Annotation validation, and asynchronous database operations with EF Core.

---

## 🚀 What It Does

The API provides full CRUD capabilities for game records and querying game genres:

- **Game Catalog Management (CRUD)**:
  - **List Games**: Fetch all games with their associated genre details formatted for summary listings.
  - **Get Game Details**: Look up detailed information for an individual game by ID.
  - **Create Game**: Add new games with payload validation (name length constraints, price ranges, genre IDs). Returns `201 Created` with a `Location` header.
  - **Update Game**: Modify existing game entries with validation, returning `204 No Content`.
  - **Delete Game**: Efficiently remove games using EF Core's `ExecuteDeleteAsync`.
- **Genre Management**:
  - **List Genres**: Query the list of game genres (Action, RPG, Multiplayer, Racing, Simulation, Story).
- **Automated Lifecycle & Seeding**:
  - Automatically applies EF Core database migrations on application startup.
  - Seeds essential genre lookup data if the database is newly initialized.
- **Input Validation**:
  - Validates request payloads against rules defined with Data Annotations before hitting the database.

---

## 🛠️ Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework / Platform** | [.NET 10](https://dotnet.microsoft.com/) / ASP.NET Core Minimal APIs |
| **Language** | C# (latest features: record types, collection expressions, primary constructors) |
| **ORM / Data Access** | [Entity Framework Core 10](https://learn.microsoft.com/ef/core/) (`Microsoft.EntityFrameworkCore.Sqlite`, `Microsoft.EntityFrameworkCore.Design`) |
| **Database** | [SQLite](https://www.sqlite.org/) (`GameStore.db`) |
| **Validation** | Data Annotations (`System.ComponentModel.DataAnnotations`) & ASP.NET Core Endpoint Validation |
| **Configuration & Hosting** | Kestrel HTTP Server, `appsettings.json`, `launchSettings.json` |
| **API Testing** | `games.http` (compatible with JetBrains Rider and VS Code REST Client) |

---

## 📂 Project Structure

```text
GameProject/
├── Data/
│   ├── GameStoreContext.cs      # EF Core DbContext definition (DbSets for Games and Genres)
│   ├── DataExtensions.cs        # DI configuration, SQLite setup, migration, and genre seeding
│   └── Migrations/              # EF Core database migration files
├── Dtos/
│   ├── CreateGameDto.cs         # Input DTO with validation attributes for creating a game
│   ├── UpdateGameDto.cs         # Input DTO with validation attributes for updating a game
│   ├── GameDetailsDto.cs        # DTO for full single-game details
│   ├── GameSummaryDto.cs        # DTO for catalog listing with mapped genre name
│   └── GenreDto.cs              # DTO for genre records
├── Enpoints/
│   ├── GamesEnpoints.cs         # Minimal API route definitions for /games (GET, POST, PUT, DELETE)
│   └── GenresEndpoint.cs        # Minimal API route definitions for /genres (GET)
├── Models/
│   ├── Game.cs                  # Game domain entity
│   └── Genre.cs                 # Genre domain entity
├── Properties/
│   └── launchSettings.json      # Development launch profiles and ports
├── appsettings.json             # Application settings and connection strings
├── games.http                   # HTTP request samples for manual API testing
├── GameStore.db                 # SQLite database file
├── GameProject.csproj           # Project configuration and package dependencies
└── Program.cs                   # Application entry point and middleware pipeline
```

---

## 🔌 API Endpoints

### Games (`/games`)

| Method | Endpoint | Description | Request Body | Response Codes |
| :--- | :--- | :--- | :--- | :--- |
| **GET** | `/games` | Retrieve all games with genre names | None | `200 OK` |
| **GET** | `/games/{id}` | Retrieve details for a single game | None | `200 OK`, `404 Not Found` |
| **POST** | `/games` | Create a new game | `CreateGameDto` (JSON) | `201 Created`, `400 Bad Request` |
| **PUT** | `/games/{id}` | Update an existing game | `UpdateGameDto` (JSON) | `204 No Content`, `404 Not Found`, `400 Bad Request` |
| **DELETE** | `/games/{id}` | Delete a game by ID | None | `204 No Content` |

### Genres (`/genres`)

| Method | Endpoint | Description | Request Body | Response Codes |
| :--- | :--- | :--- | :--- | :--- |
| **GET** | `/genres` | Retrieve all available genres | None | `200 OK` |

---

## 📋 Sample Request Payloads

### Create a Game (`POST /games`)

```json
{
  "name": "The Witcher 3: Wild Hunt",
  "genreId": 2,
  "price": 39.99,
  "releaseDate": "2015-05-19"
}
```

### Update a Game (`PUT /games/{id}`)

```json
{
  "name": "The Witcher 3: Wild Hunt - Complete Edition",
  "genreId": 2,
  "price": 49.99,
  "releaseDate": "2015-05-19"
}
```

---

## 🏁 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) or later installed on your system.
- An IDE such as [JetBrains Rider](https://www.jetbrains.com/rider/), [Visual Studio 2022+](https://visualstudio.microsoft.com/), or [VS Code](https://code.visualstudio.com/) (with C# Dev Kit).

### Installation & Run

1. **Clone or navigate to the repository**:
   ```bash
   cd GameProject
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

4. **Run the application**:
   ```bash
   dotnet run
   ```

The application starts on the URLs specified in `Properties/launchSettings.json`:
- HTTP: `http://localhost:5114`
- HTTPS: `https://localhost:7163`

> **Note**: Database creation and migrations run automatically on startup via `app.MigrateDb()`. The initial genres are automatically seeded into `GameStore.db`.

---

## 🧪 Testing the API

### Using the `.http` File

If you are using JetBrains Rider, Visual Studio, or VS Code (with the REST Client extension), open [games.http](file:///home/not_illusionaire/Documents/Projects/ASP.NET/GameProject/games.http) to execute and test pre-configured requests directly against `http://localhost:5114`.

### Using `curl`

```bash
# Get all games
curl -i http://localhost:5114/games

# Get all genres
curl -i http://localhost:5114/genres

# Create a game
curl -i -X POST http://localhost:5114/games \
  -H "Content-Type: application/json" \
  -d '{"name":"Cyberpunk 2077","genreId":2,"price":59.99,"releaseDate":"2020-12-10"}'
```
