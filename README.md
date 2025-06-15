# Ecommerce Record Shop
The project is a full-stack web application that allows users to browse and purchase music items.
You need a SQL Server database to use the app. All of the data is provisioned by the application with EF Core. Connection string is configured in the API/appsettings.json file.
Required dependencies:
- .NET 8
- Angular CLI
- SQL Server instance
To run the API, navigate to the API directory and execute the following command:
```
dotnet run .
```
To run the UI, navigate to RecordStoreUI directory and execute the command:
```
ng serve --ssl .
```
