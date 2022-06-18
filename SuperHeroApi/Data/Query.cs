using GraphQL;
using Microsoft.AspNetCore.Identity;
using SuperHeroApi.Consts;
using SuperHeroApi.Models;

namespace SuperHeroApi.Data;

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
            (s.Height > 100 || isLevel3Admin)

        );
    }
}