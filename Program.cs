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

            var app = builder.Build();

            var sampleTodos = new Todo[] {
                new(0, "Do the whole zoo even the janitor", DateOnly.FromDateTime(DateTime.Now)),
                new(0, "Do the doggie", DateOnly.FromDateTime(DateTime.Now)),
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
            app.Run();
        }
    }
}
