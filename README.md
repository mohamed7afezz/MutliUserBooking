# Multi-User Booking Web API

This is a basic multi-user booking web API built using ASP.NET Core 7 and Entity Framework 7. It provides a set of RESTful APIs for managing reservations for trips.

## Features

- Sead 1000 User and Trip using Bogus
- CRUD operations for reservations
- Seamless integration with Swagger for API documentation
- CQRS pattern implementation using MediatR
- Domain-Driven Design approach
- Fluent Validations for API input validations
- Project structure follows the Onion layer architecture

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)

## Getting Started

1. Clone the repository:

## Getting Started

1. Clone the repository: 
`git clone https://github.com/mohamed7afezz/MutliUserBooking.git`

2. Navigate to the project directory:
`cd booking-api`

3. Open the project in your preferred development environment (e.g., Visual Studio, Visual Studio Code).

4. If you will go deploy this project to server and will not use localy, modify the `appsettings.json` file to configure the database connection and other settings.

5. Build the project:
`dotnet build`

6. Apply database migrations:
`dotnet ef database update`

7. Run the application:
`dotnet run`

8. The API will be accessible at `https://localhost:44378` (or `http://localhost:44378`).

## API Endpoints

The API endpoints can be explored and tested using the Swagger UI. Once the application is running, open your web browser and navigate to `https://localhost:44378/swagger` (or `http://localhost:44378/swagger`).

The following endpoints are available:

- GET /api/reservations: Get a list of all reservations.
- GET /api/reservations/{id}: Get details of a specific reservation.
- POST /api/reservations: Create a new reservation.
- PUT /api/reservations/{id}: Update an existing reservation.
- DELETE /api/reservations/{id}: Delete a reservation.

## Project Structure

The project follows the Onion layer architecture, which promotes a modular and decoupled design. The main layers include:
![Onion Architecture](https://www.clarity-ventures.com/portals/0/images/articles/888/onion-arch.jpg)

- **Core**: Contains the domain models, interfaces, and shared application logic.
- **Infrastructure**: Provides implementations for data access, external services, and other infrastructure concerns.
- **Application**: Contains application services, command/query handlers, and other application-specific logic.
- **API**: Implements the API endpoints and acts as the entry point for client applications.

## Authors

- Mohamed Hafez <mohamed7afezz@gmail.com>

## License

This project is licensed under the [MIT License](LICENSE).

Feel free to customize this documentation based on your project requirements and add any additional sections or information as needed.
