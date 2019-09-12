# BrewRides

CheatSheet 

## Docker

> sudo docker run -e "ACCEPT_EULA=Y" -p 1433:1433 -e "SA_PASSWORD=<PASSWORD>" -d microsoft/mssql-server-linux:latest

## Pages
Creating a new Razer view in a subdirectory
> dotnet new page -n CustomLogin -o Pages/Account/

## Database
 Creating a new migration
 > dotnet ef migrations add <Name> --context <ContextName>

 To commit the change,
 > dotnet ef database update --context <ContextName>

 ### Contexts
  2 Contexts exist, RouteDatabaseContext and BrewRideUserDbContext

  ### Setting Connection String
  > dotnet user-secrets set ConnectionStrings:BrewRidesDatabaseContext Server=localhost,1433; Database=BrewRides; User=sa; Password=<PASSWORD>;