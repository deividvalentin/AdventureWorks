# Order API

### Prerequisites
You will need the following tools:

* [AdventureWorks2019 Database](https://github.com/Microsoft/sql-server-samples/releases/download/adventureworks/AdventureWorks2019.bak)
* [Visual Studio Code](https://code.visualstudio.com/download)
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Building and Running
---
 * Under appsettings update the *ConnectionString* with correct value to your database
 > Note: if debug mode, update the *ConnectionString* under appsettings.Development.json
 * Build the Adventure works solution: dotnet build AdventureWorks.sln
 * To run: dotnet run ./src/API/API/csproj
 * To run unit tests: dotnet test AdventureWorks.sln

## Additional Information (Assignment)
---
Due to a short time, there are missing validations in the API/Models and Services Layer.  
Also, the unit tests could not have been completed.

## Architecture: 
* Architecture: Domain Driven Design
* Persistance: Dapper, Dapper-Plus
* Framework: .NET Core 3.1
* Language: C# 8.0
* Database: SQL Server
* API Documentation: Swagger
* Class Mapping: AutoMapper
* Mocking Framework: Moq
* API Architecture: RESTFul
* Test Framework: Xunit


## Project Structure
---
- Source
  -  API Layer
  -  Data Layer
  -  Domain Layer
  -  Services Layer
- Tests
  -  UnitTests
     - API.Tests
     - Data.Tests 
     - Services.Tests 

## Swagger API Documentation
https://localhost:5001/swagger/index.html