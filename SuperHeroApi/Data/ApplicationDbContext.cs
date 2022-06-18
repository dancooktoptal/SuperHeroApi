using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperHeroApi.Data.ContextConfigurations;
using SuperHeroApi.Models;
namespace SuperHeroApi.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Generate three GUIDS and place them in an arrays
        var ids = new Guid[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

        // Apply configuration for the three contexts in our application
        // This will create the demo data for our GraphQL endpoint.
        builder.ApplyConfiguration(new SuperheroContextConfiguration(ids));
        builder.ApplyConfiguration(new SuperpowerContextConfiguration(ids));
        builder.ApplyConfiguration(new MovieContextConfiguration(ids));
        builder.ApplyConfiguration(new IdentityContextConfiguration());


    }
    // Add the DbSets for each of our models we would like at our database
    public DbSet<Superhero> Superheroes { get; set; }
    public DbSet<Superpower> Superpowers { get; set; }
    public DbSet<Movie> Movies { get; set; }
}