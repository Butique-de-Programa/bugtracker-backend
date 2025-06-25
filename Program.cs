using Microsoft.EntityFrameworkCore;
using BugTracker.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BugTrackerContext>(opt =>
    opt.UseInMemoryDatabase("BugTrackerList"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
        options.Path = "";
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();