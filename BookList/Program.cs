using Microsoft.EntityFrameworkCore;
using Npgsql;
using DotNetEnv;
using BookList.Application.Services;
using BookList.Services.Interfaces;
using BookList.Repository;
using BookList.Repository.Interfaces;
using BookList.Data;
using BookList.Domain;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("🚀 Main method is executing");
        
        // Load environment variables
        DotNetEnv.Env.Load();
        var connectionString = GetDatabaseConnectionString();

        // Test database connection
        TestDatabaseConnection(connectionString);

        // Build WebApplication
        var builder = WebApplication.CreateBuilder(args);
        ConfigureLogging(builder);
        
        // Register services
        ConfigureServices(builder);

        // Build app
        var app = builder.Build();

        // Configure the middleware pipeline
        ConfigureMiddleware(app);
        
        // 🛡️ Enable CORS
        app.UseCors("AllowFrontend");

        ApplyMigrations(app);

        Console.WriteLine("📚 BookList API configured and ready");

        // Run the application
        app.Run("http://0.0.0.0:9090");
    }

    // Get the database connection string from environment variables
    private static string GetDatabaseConnectionString()
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var db = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) ||
            string.IsNullOrEmpty(db) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ Missing one or more required .env variables.");
            Console.ResetColor();
            Environment.Exit(1); // Exit the application
        }

        return $"Host={host};Port={port};Database={db};Username={user};Password={pass}";
    }

    // Test connection to the PostgreSQL database
    private static void TestDatabaseConnection(string connStr)
    {
        Console.WriteLine("🔌 Testing database connection...");
        try
        {
            using var conn = new NpgsqlConnection(connStr);
            conn.Open(); // This will throw an exception if the container is not running
            Console.WriteLine("✅ Successfully connected to PostgreSQL.");
            conn.Close();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ Could not connect to PostgreSQL: {ex.Message}");
            Console.ResetColor();
            Environment.Exit(1); // Exit if unable to connect to the database
        }
    }

    private static void ConfigureLogging(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();  // Clear default loggers
        builder.Logging.AddConsole();      // Add console logger
        builder.Logging.SetMinimumLevel(LogLevel.Debug); // Set minimum log level
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<BookContext>(options =>
            options.UseNpgsql(GetDatabaseConnectionString()));

        // Register repositories
        builder.Services.AddScoped<IAuthorRepo, AuthorRepo>();
        builder.Services.AddScoped<IBookRepo, BookRepo>();
        builder.Services.AddScoped<IPublisherRepo, PublisherRepo>();

        builder.Services.Configure<FeatureToggles>(builder.Configuration.GetSection("FeatureToggles"));

        // Register application services
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IPublisherService, PublisherService>();

        // 🛡️ Add CORS
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins("http://localhost:3000") // your frontend URL
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Add controllers
        builder.Services.AddControllers();

        // Add Swagger (OpenAPI) support
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    // Configure middleware pipeline
    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();          // Enable Swagger in development
            app.UseSwaggerUI();       // Swagger UI to explore the API
        }

        app.UseCors("AllowLocalhost3000");
        app.UseHttpsRedirection();    // Ensure secure connections
        app.UseAuthorization();       // Use authorization middleware (can be expanded with authentication)
        app.MapControllers();         // Map the controller routes
    }

    public static void ApplyMigrations(WebApplication app)
    {
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<BookContext>();
        dbContext.Database.Migrate(); // Applies migrations
        Console.WriteLine("✅ Migrations applied.");
    }
    }
}
