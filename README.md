# ProjectManagement Management System

Run: dotnet run --project src/ProductManagement/ProductManagement.API.csproj

Test: dotnet test


## Project Structure
1. ProjectManagement.Data - Contains the domain models, DTOs, Exceptions, Repositories, Mappers, and Validators
2. ProjectManagement.Services - The service layer for the application that contains the business logic
3. ProjectManagement.API - WebApi project that contains the controllers to invoke services
4. ProjectManagement.Tests - Unit tests
