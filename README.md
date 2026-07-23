# RentDriveAPI 🚗

A RESTful Vehicle Rental API built with ASP.NET Core 10 and Entity Framework Core.

## Features
- 🚗 **Vehicles** — Full CRUD with daily rate management
- 📅 **Bookings** — Create bookings with date validation
- 🔒 **Overlap Detection** — Prevents double booking of vehicles
- 💰 **Auto Price Calculation** — Total price = days × daily rate
- 🔍 **Availability Filter** — Find available vehicles for any date range
- 🌐 **CORS** — Enabled for frontend integration

## Tech Stack
| Layer | Technology |
|-------|------------|
| Framework | ASP.NET Core 10 |
| ORM | Entity Framework Core |
| Database | SQL Server |

## Endpoints
### Vehicles
- `GET /api/vehicle` — Get all vehicles
- `GET /api/vehicle/{id}` — Get vehicle by ID
- `GET /api/vehicle/available?startDate=&endDate=` — Get available vehicles
- `POST /api/vehicle` — Add vehicle
- `PUT /api/vehicle/{id}` — Update vehicle
- `DELETE /api/vehicle/{id}` — Delete vehicle

### Bookings
- `GET /api/booking/vehicle/{vehicleId}` — Get bookings for a vehicle
- `POST /api/booking` — Create booking
- `DELETE /api/booking/{vehicleId}/{startDate}/{endDate}` — Cancel booking

## Setup
1. Clone the repo
2. Update connection string in `appsettings.json`
3. Run `dotnet ef database update`
4. Run `dotnet run`
