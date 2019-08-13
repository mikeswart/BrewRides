# BrewRides

CheatSheet 

Creating a new Razer view in a subdirectory
> dotnet new page -n CustomLogin -o Pages/Account/

##Database

 Creating a new migration
 > dotnet ef migrations add <Name> --context <ContextName>

 To commit the change,
 > dotnet ef database update --context <ContextName>

 ### Contexts
  2 Contexts exist, RouteDatabaseContext and BrewRideUserDbContext