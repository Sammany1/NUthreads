using Microsoft.OpenApi.Models;
using NUthreads.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB settings
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Get MongoDB settings for infrastructure registration
var mongoDbSettings = builder.Configuration
    .GetSection("MongoDBSettings")
    .Get<MongoDBSettings>();

var mongoDbSettingsOptions = Microsoft.Extensions.Options.Options.Create(mongoDbSettings);

// Add infrastructure services (including MongoDB DbContext)
builder.Services.AddInfrastructure(
    mongoDbSettingsOptions);

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

// Add global exception handling
app.UseExceptionHandler("/error");

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