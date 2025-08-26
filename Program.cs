using SpeedApply.Api.Interfaces;
using SpeedApply.Api.Models;
using SpeedApply.Api.Repositories;
using SpeedApply.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
 
//project namespace
namespace SpeedApply
{
    public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);
    [JsonSerializable(typeof(Todo[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
    public static class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext with PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Dependency Injection for Repository & Service
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            builder.Services.AddControllers();

            var app = builder.Build();



            /**
             * Testing connection code BEGINNING
             */

            var sampleTodos = new Todo[] {
                new(0, "Do the snake", DateOnly.FromDateTime(DateTime.Now)),
                new(1, "Do the poodle", DateOnly.FromDateTime(DateTime.Now)),
                new(2, "Do the billy goat", DateOnly.FromDateTime(DateTime.Now)),
                new(3, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
                new(4, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                new(5, "Clean the bathroom"),
                new(6, "Do my little pony", DateOnly.FromDateTime(DateTime.Now)),
                new(7, "Do my huge zebra", DateOnly.FromDateTime(DateTime.Now)),
                new(8, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
            };

            var todosApi = app.MapGroup("/todos");

            todosApi.MapGet("/", () => sampleTodos);

            todosApi.MapGet("/{id}", (int id) =>
                sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
                    ? Results.Ok(todo)
                    : Results.NotFound());

            Random random = new Random();
            int randomNumber = random.Next(); // Generates a non-negative random integer
            string randomNumberString = randomNumber.ToString();

            using (var context = new AppDbContext())
            {
                // Example: Add a new user
                var newUser = new Users { UserName = "testuser0" + randomNumberString, Email = randomNumberString + "test@example.co", Password = "password0" };
                context.Users.Add(newUser);
                context.SaveChanges();
                Console.WriteLine("User added successfully!");

                // Example: Query users
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"User ID: {user.Id}, Username: {user.UserName}, Email: {user.Email}");
                }
            }

            /**
             * Testing connection code ENDING
             */

            app.MapControllers();
            app.Run();
        }
    }
}
