using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;


//Create builder
var builder = WebApplication.CreateBuilder(args);

//Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// env, if exist
if (File.Exists(".env"))
{
    DotNetEnv.Env.Load();
}

// Database connection, prioritize .env if not, go to appsetting
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION")
                          ?? builder.Configuration.GetConnectionString("DefaultConnection");


// JWT configuration, prioritize .env if not, go to appsetting
var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY")
          ?? builder.Configuration["JwtKey"];


// DBcontext Configuration
builder.Services.AddDbContext<BugTrackerContext>(options =>
    options.UseNpgsql(connectionString));

// Identity configuration; User, Roles
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BugTrackerContext>()
    .AddDefaultTokenProviders();

// JWT Configuration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]!))
    };
});

// Automapper (Profile -> DTOs)
builder.Services.AddAutoMapper(typeof(Program));

// Swagger
builder.Services.AddOpenApi();

// Add endpoints
builder.Services.AddControllers();

// Build App
var app = builder.Build();



// Pipeline requirements
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
        options.Path = "";
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();