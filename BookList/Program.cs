using Microsoft.EntityFrameworkCore;
using Npgsql;
using DotNetEnv;
using BookList.Application.Services;
using BookList.Services.Interfaces;
using BookList.Repository;
using BookList.Repository.Interfaces;
using BookList.Data;
using BookList.Domain;



public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("🚀 Main method is executing");
        DotNetEnv.Env.Load();
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var db   = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");
        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) ||
            string.IsNullOrEmpty(db) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Missing one or more required .env variables.");
            Console.ResetColor();
            Environment.Exit(1);
        }
        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pass}";
        // ✅ Connection test
        TestDatabaseConnection(connectionString);
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
        builder.Services.AddDbContext<BookContext>(options =>
            options.UseNpgsql(connectionString));
        
         // Register BookRepo
        builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
        builder.Services.AddScoped<BookRepo>();
        builder.Services.AddScoped<PublisherRepo>();

        builder.Services.AddScoped<IAuthorService, AuthorService>();

        // Add controller support
        builder.Services.AddControllers();
        
        // Add swagger documentation
        // Add Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Enable Swagger middleware
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        
        // Basic route
        app.MapGet("/", () => "Hello World! BookList API is running.");
        
        // Books API endpoint
        app.MapGet("/api/books", async (BookContext context) => 
            await context.Books.ToListAsync());
            
        app.MapGet("/api/books/{id}", async (int id, BookContext context) => 
            await context.Books.FindAsync(id) is Book book ? 
                Results.Ok(book) : Results.NotFound());
                
        Console.WriteLine("📚 BookList API configured and ready");

    

        app.Run();
    }
    
    private static void TestDatabaseConnection(string connStr)
    {
        Console.WriteLine("🔌 Testing database connection...");
        try
        {
            using var conn = new NpgsqlConnection(connStr);
            conn.Open(); // this will throw if the container is not running
            Console.WriteLine("✅ Successfully connected to PostgreSQL.");
            conn.Close();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Could not connect to PostgreSQL: {ex.Message}");
            Console.ResetColor();
            Environment.Exit(1); // kill app
        }
    }
}
