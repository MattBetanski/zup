using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

using webapi.Models;
using webapi.Data;
using webapi.Services;
using webapi.Identity;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// load db
DotNetEnv.Env.Load(); // load .env file for db connection string
builder.Services.AddDbContext<ZupContext>(options =>
    options.UseNpgsql(Environment.GetEnvironmentVariable("DEFAULT_POSTGRES"))
);

// session management
builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y => {
    y.TokenValidationParameters = new TokenValidationParameters {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")!)),
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy(IdentityData.AdminUserPolicyName, p => 
        p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
});


// Add services to the container.
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
