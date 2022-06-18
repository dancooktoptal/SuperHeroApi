using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperHeroApi.Models;
namespace SuperHeroApi.Data.ContextConfigurations;

public class IdentityContextConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder
            .HasData(
                new IdentityRole
                {
                    Id = "4f9c41ec-780e-4a18-8de1-4f3d5b23ef31",
                    Name = "Level3Admin",
                    NormalizedName = "LEVEL3ADMIN"
                });
    }
}