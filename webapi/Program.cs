using Microsoft.EntityFrameworkCore;
using System.Reflection;
using webapi.Models;
using webapi.Data;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ZupContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres_default_db"))
);

//builder.Services.AddScoped<ProjectService>();
//builder.Services.AddScoped<DepartmentService>(); 
var services = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => t.Namespace != null && t.Namespace.StartsWith("webapi.Services") && t.Name.EndsWith("Service"))
    .ToList();

foreach (var service in services)
{
    builder.Services.AddScoped(service);
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
