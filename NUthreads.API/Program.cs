
using Microsoft.OpenApi.Models;
using NUthreads.Infrastructure;
using NUthreads.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Load environment-specific configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));
// DEPENDENCY INJECTION
builder.Services.AddInfrastructure();
// Configure MongoDB mappings
MongoDbMappings.Configure();

// Add controllers and API explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NUThreads API",
        Version = "v1",
        Description = "API for managing users in NUThreads.",
    });
});


var app = builder.Build();

// Enable Swagger for development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NUThreads API V1");
        options.RoutePrefix = string.Empty;
    });
}


// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable CORS (if needed)
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Enable authorization
app.UseAuthorization();

// Map controllers
app.MapControllers();


app.Run();