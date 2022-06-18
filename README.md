# GraphQL Row Level Security (Superhero Demo)

Web API project with GraphQL / Entity Framework / SQL / .NET Identity Authorisation demonstrating row-level security of GraphQL queries based on Role.

## Stack

.NET 6
Microsoft SQL Database (localdb)
Entity Framework Core 6
.NET Identity
Graph QL
Hot Chocolate UI for Graph SQL

## Supports

- Projections
- Filtering
- Sorting
- Authentication
- Authorization
- Row-level security (filtering data by role)

## Quick Start

1. Clone the repo
2. Update your localdb connection string in `appsettings.json` if necessary
3. Migrate and seed the database with `update-database`
4. Run the web app
5. Register a new account with any email address (no email confirmation required)

![image](https://user-images.githubusercontent.com/80036622/174424113-7a8ff064-ca6c-44f4-a774-88776d83626f.png)

6. You will be redirected to Hot Chocolate GraphQL UI
7. Run the following query

```graphql
query{
  superheroes
  {
    name
    height
  }
}
```

## Expected Result

Only "Batman" will be returned because "Level3Admin" role is required to see Superheroes above 1.8m in height).

![image](https://user-images.githubusercontent.com/80036622/174423948-b8323cae-5c52-4502-b1aa-a47dd4bbde42.png)

## Level 3 Admin Access

1. Open SQL Server Management Studio
2. Add your user to the "Level3Admin" role in the `AspNetUserRoles` table. Role ID is: `4f9c41ec-780e-4a18-8de1-4f3d5b23ef31`

![image](https://user-images.githubusercontent.com/80036622/174424140-9193fcf8-282e-4f97-94ca-14eb7cc7e8c5.png)

3. Restart the application
4. Clear your Cookies in Chrome Tools in order to "Log out"
5. Log back in again with your user
6. Run the same query

```graphql
query{
  superheroes
  {
    name
    height
  }
}
```

## Expected Result

This time all of the Superheroes are returned because you are a "Level3Admin"

![image](https://user-images.githubusercontent.com/80036622/174423914-ed2cfe64-c14d-4754-a65a-3f9f60fb21f7.png)


# How row level security works

In the resolver for the query (`Query.cs`), we get the user (from `IHttpContextAccessor`) and look up the roles.
We can then filter the EF Linq query in the usual way:

```csharp
[Authorize]
public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public async Task<IQueryable<Superhero>> GetSuperheroes(
        [Service] ApplicationDbContext context, 
        [Service]UserManager<User> userManager, 
        [Service]IHttpContextAccessor httpContextAccessor)
    {
        var user = await userManager.FindByNameAsync(httpContextAccessor.HttpContext.User.Identity.Name);
        var isLevel3Admin = await userManager.IsInRoleAsync(user, Roles.Level3Admin);

        return context.Superheroes.Where(s =>

            // Row level security filtering based on role
            (s.Height > 1.8 || isLevel3Admin)

        );
    }
}
```

# References

Tutorial on GraphQL with Entity Framework
https://christian-schou.dk/how-to-implement-graphql-in-asp-net-core/

How to implement authorization using GraphQL.NET at Resolver function level?
https://stackoverflow.com/questions/53537521/how-to-implement-authorization-using-graphql-net-at-resolver-function-level

GraphQL: Simple authorization and authentication with HotChocolate 11 and ASP.NET Core 3
https://medium.com/@marcinjaniak/graphql-simple-authorization-and-authentication-with-hotchocolate-11-and-asp-net-core-3-162e0a35743d

graphql-dotnet Authorization - Getting Started
https://graphql-dotnet.github.io/docs/getting-started/authorization/
