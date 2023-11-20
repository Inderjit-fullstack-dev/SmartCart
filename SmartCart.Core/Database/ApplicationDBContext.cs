using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCart.Core.Constants;
using SmartCart.Core.Entities;

namespace SmartCart.Core.Database
{
    public class ApplicationDBContext(DbContextOptions options) 
        : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(b =>
            {
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                b.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            SeedData(builder);

        }

        private static void SeedData(ModelBuilder builder)
        {

            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole { Name = RoleConstants.Admin, NormalizedName = RoleConstants.Admin, ConcurrencyStamp="1"},               
                new IdentityRole { Name = RoleConstants.Customer, NormalizedName = RoleConstants.Customer, ConcurrencyStamp = "2" }               
            );
        }
    }
}
