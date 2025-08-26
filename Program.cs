using SpeedApply.Api.Data;
using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Repositories;
using SpeedApply.Api.Services;

//project namespace
namespace SpeedApply
{
    
    public static class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateSlimBuilder(args);

            // Register DbContext with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>();

            // Dependency Injection for Repository & Service
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            Console.WriteLine("API TESTING 1");

            builder.Services.AddControllers();

            var app = builder.Build();

            app.MapControllers();
            app.Run();
        }
    }
}
