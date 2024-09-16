#  Management System

Run: dotnet run --project src/ProductManagement.API

Test: dotnet test


## Project Structure
1. ProductManagement.Data - Contains the domain models, DTOs, Exceptions, Repositories, Mappers, and Validators
2. ProductManagement.Services - The service layer for the application that contains the business logic
3. ProductManagement.API - WebApi Product that contains the controllers to invoke services
4. ProductManagement.Tests - Unit tests
