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


//********************builder**************************//
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
//string SqliteConnectionString = config.GetConnectionString("SqliteConnection") ?? "";
string sqlConnectionString = config.GetConnectionString("SqlServerConnection") ?? "";
//string mongoDbConnectionString = config.GetConnectionString("MongoDbConnection");

// sqlite
/*
builder.Services.AddDbContext<MemberDbContext>(
    options => options.UseSqlite(SqliteConnectionString));
*/
// Call EnsureCreated during application startup

//sql server

builder.Services.AddDbContext<MemberDbContext>(
    options => options.UseSqlServer(sqlConnectionString));


using (var serviceScope = builder.Services.BuildServiceProvider().CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<MemberDbContext>();
    dbContext.Database.EnsureCreated();
}
//var mongoClient = new MongoClient(mongoDbConnectionString);
//builder.Services.AddSingleton<IMongoClient>(mongoClient);

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
