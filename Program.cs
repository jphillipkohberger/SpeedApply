using Microsoft.AspNetCore.Identity;
using SpeedApply.Api.Data;
using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Dtos;
using SpeedApply.Api.Repositories;
using SpeedApply.Api.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

//project namespace
namespace SpeedApply
{
    
    public static class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateSlimBuilder(args);

            // Add CORS services and define a policy
            var SpeedApplyCorsPolicy = "SpeedApplyCorsPolicy";
            builder.Services.AddCors(options => {
                options.AddPolicy(name: SpeedApplyCorsPolicy,
                    policy => {
                        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost") // Frontend React 
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";    // redirect if not logged in
                options.LogoutPath = "/Account/Logout";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
            });

            builder.Services.AddAuthorization();

            // register password hashing
            builder.Services.AddScoped<IPasswordHasher<UsersDto>, PasswordHasher<UsersDto>>();

            // Register DbContext with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>();

            // Dependency Injection for Repository & Service
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            /**
             * The call to UseCors must be placed after UseRouting,
             * but before UseAuthorization. For more information,
             * see Middleware order.
             * https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-9.0#middleware-order
             */
            app.UseCors(SpeedApplyCorsPolicy);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
