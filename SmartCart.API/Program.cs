using SmartCart.Infrastructure;
using SmartCart.Application;
using SmartCart.Core.Database;
using Microsoft.EntityFrameworkCore;
using SmartCart.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace SmartCart.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddDbContext<ApplicationDBContext>(option =>
                {
                    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
                });

            builder.Services.AddIdentity<User, IdentityRole>(options => 
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedAccount = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // password options
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddApplication().AddInfrastructure();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
