# Developer Evaluation Project

This application manages sales operations using a clean architecture. It  uses MediatR and AutoMapper for request handling and mapping.

## Instructions

### How to Run the Application

**Prerequisites**
- .NET 8 SDK
- Postgres 

**Getting Started**

- Clone the repository<br>

``` 
git clone https://github.com/pablorzenha/Ambev.git
```
``` 
cd Ambev
```

- Set up your connection string: <br>
Edit the appsettings.json file and update the database connection string, example:<br>

```
"ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=AmbevDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
```

- Install the EF CLI tool if you haven’t yet:<br>

``` 
dotnet tool install --global dotnet-ef
```  

- Run database migrations

```
dotnet ef database update --project Ambev.DeveloperEvaluation.ORM/ --startup-project Ambev.DeveloperEvaluation.WebApi/
```
- Run Project<br>
```
cd src/Ambev.DeveloperEvaluation.WebApi/
```
- Set up your applicationUrl in launchSettings.json.<br>
```
dotnet run
```

## Documentation
This project provides full documentation to help you understand, configure, and test the API.

The detailed description of the API endpoints is available in the following file:<br>
```
./.doc/sale-api.md
```

There you'll find the orientation to complete CRUD for sales

### Swagger
The project uses Swagger to expose interactive API documentation.<br>
```
http://localhost:{PORT}/swagger
```

## Test

This project contains unit tests.

- To run
  
```
cd tests/Ambev.DeveloperEvaluation.Unit/
```

