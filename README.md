Welcome to Won# WON
WON is an abbreviation of "Whether Or Not" - the full name for the project

A full-stack travel planning application built with ASP.NET Core, Blazor, Entity Framework Core, SQL Server, Docker, and Google Gemini AI.

WON allows users to plan and manage trips, track travel details, view destination information and weather forecasts, and generate AI-powered activity suggestions tailored to their travel plans.

---

## Features

### Authentication

* User registration
* Secure user login
* Password hashing
* JWT authentication
* Protected API endpoints

### Trip Management

* Create trips
* View all trips
* View detailed trip information
* Update existing trips
* Delete trips
* Budget tracking
* Group size management
* Travel notes and itinerary details

### Destination Information

* Destination imagery
* Destination descriptions
* Travel information suggestions

### Weather Forecasting

* Weather forecasts for upcoming trips 
* Forecasts displayed when trip dates fall within the supported forecast period (14 days)

### AI Activity Suggestions

* AI-powered travel activity recommendations (via Google Gemini free-tier API)
* Suggestions tailored to:
  * Destination
  * Travel dates
  * Budget
  * Group size

---

## Technology Stack

### Frontend

* Blazor Web App
* Razor Components
* C#
* Bulma CSS

### Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core

### Database

* SQL Server
* EF Core Migrations

### AI & External Services

* Google Gemini API
* Weather Forecast API
* Location Image Services

### Infrastructure

* Docker
* Docker Compose

---

## Project Architecture

### Won.Api

Backend Web API responsible for:

* Authentication
* Trip management
* Activity management
* AI recommendation generation
* Weather forecasting
* Database access

Key components:

Controllers/
├── ActivitiesController
├── AiActivitySuggestionsController
├── AuthController
├── RecommendationsController
├── TripsController
└── WeatherForecastController

Services/
├── ActivityService
├── AuthService
├── TripService
├── ActivityRecommendationService
└── GeminiActivitySuggestionService

Data/
└── WonDbContext


### Won.Web

Blazor frontend responsible for:

* User interface
* User interactions
* API communication
* Trip management workflows

Frontend services:

Services/
├── Activities
├── Locations
├── Trips
└── Weather


### Won.Shared

Shared project containing:

* DTOs
* Common response models
* Shared contracts between frontend and backend

---

## Solution Structure

Won.sln

src/
├── Won.Api
├── Won.Shared
├── Won.Web
└── Won.Web.Client

tests/

---

## Database Schema

Core entities:

### User

Stores:

* User details
* Login credentials
* Authentication data

### Trip

Stores:

* Trip name
* Destination
* Start date
* End date
* Budget
* Group size
* Trip details

### Activity

Stores:

* Suggested activities
* Recommendations
* Trip-related activities

---

## Getting Started

### Prerequisites

Install:

* .NET 8 SDK
* Docker Desktop
* Git

### Clone Repository

```bash
git clone <repository-url>
cd Won
```

### Run Using Docker

```bash
docker compose up --build
```

### Apply Database Migrations

```bash
dotnet ef database update
```

### Run Application

Frontend:

```text
http://localhost:7003
```

API:

```text
Configured via Docker Compose
```

---

## Development Workflow

Create a feature branch:

```bash
git checkout -b feature-name
```

Commit changes:

```bash
git add .
git commit -m "description of changes"
```

Push branch:

```bash
git push origin feature-name
```

Create a Pull Request and obtain approval before merging into main.

---

## Future Enhancements

* Favourite trips
* User profiles
* Shared trips
* Trip collaboration
* Advanced itinerary planning
* Maps integration
* Booking integrations
* Personalised AI travel assistants

---

## Team

Developed as part of the Northcoders Software Development Bootcamp.

Contributors:
Zitian Harper
https://github.com/zitianharper
Istvan Vas
https://github.com/vasswann
Tom Lenton
https://github.com/tomlenton
Kati Stark
http://github.com/layyzzee
Lauraine Griffins
https://github.com/Meanderling
---

## License

This project was created for educational purposes as part of the Northcoders Software Development Bootcamp.
