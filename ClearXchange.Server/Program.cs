using ClearXchange.Server.Data;
using ClearXchange.Server.Interfaces;
using ClearXchange.Server.Middleware;
using ClearXchange.Server.Services;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

//********************builder**************************//
var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

//********sqlite*************
string SqliteConnectionString = config.GetConnectionString("SqliteConnection") ?? "";
builder.Services.AddDbContext<MemberDbContext>(
    options => options.UseSqlite(SqliteConnectionString));

//*******sql server*************
/*
string sqlConnectionString = config.GetConnectionString("SqlServerConnection") ?? "";
builder.Services.AddDbContext<MemberDbContext>(
    options => options.UseSqlServer(sqlConnectionString));
*/


using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<MemberDbContext>();
    dbContext.Database.EnsureCreated();
}

//*******mongodb*************
/*
string mongoDbConnectionString = config.GetConnectionString("MongoDbConnection");
var mongoClient = new MongoClient(mongoDbConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);
*/
builder.Services.AddDbContext<MemberDbContext>(options =>
{
    options.UseLoggerFactory(builder.Services.BuildServiceProvider().GetRequiredService<ILoggerFactory>());
});


// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(MemberSQLRepository<>));
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();
builder.Services.AddLogging(options =>
{
    options.AddConsole(); 
});

//security
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "clearxchange-issuer",
        ValidAudience = "clearxchange-audience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("clearxchange-secret-key"))
    };
});

//****************************************//
builder.Services.AddSwaggerGen(c =>
{
    // Add security definitions (if using JWT, for example)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Include the security requirement globally for all endpoints
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
                });
});




//****************************************//
/*
builder.Services.AddSwaggerGen(c =>
{
    // Configure JWT authentication for Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authorization",
        Description = "Enter your JWT token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
*/
builder.Services.AddSingleton<IJwtService, JwtService>();
//**********************************************//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware <LoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseLoggingMiddleware();
app.Run();