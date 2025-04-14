# Developer Evaluation Project

## Instructions

### How to Run the Application

**Prerequisites**
- .NET 8 SDK
- Postgres 

**Getting Started**

- Clone the repository
   git clone https://github.com/pablorzenha/Ambev.git
   cd Ambev
  
- Set up your connection string:
  Edit the appsettings.json file and update the database connection string, example:
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=AmbevDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }

- Install the EF CLI tool if you haven’t yet:
  dotnet tool install --global dotnet-ef
  
- Run database migrations
  dotnet ef database update --project Ambev.DeveloperEvaluation.ORM/ --startup-project Ambev.DeveloperEvaluation.WebApi/

- Run Project
  cd src/Ambev.DeveloperEvaluation.WebApi/
  - Set up your applicationUrl in launchSettings.json. 
  dotnet run
  
