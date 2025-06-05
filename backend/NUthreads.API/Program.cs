using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NUthreads.Application.Interfaces.Repositories;
using NUthreads.Infrastructure;
using System.Text;

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
builder.Services.AddInfrastructure(mongoDbSettingsOptions);

MongoDbMappings.Configure();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])
        ),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = async context =>
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var revokedTokenRepo = context.HttpContext.RequestServices.GetRequiredService<IRevokedTokenRepository>();

            if (await revokedTokenRepo.IsTokenRevokedAsync(token))
            {
                context.Fail("Token has been revoked.");
            }
        }
    };
});

builder.Services.AddAuthorization();

// Add controllers and API explorer
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
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

// Enable authorization & authentication
app.UseAuthentication();
app.UseAuthorization();


// Map controllers
app.MapControllers();


app.Run();