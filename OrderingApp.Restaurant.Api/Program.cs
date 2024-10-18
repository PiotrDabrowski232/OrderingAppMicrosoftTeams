using Microsoft.EntityFrameworkCore;
using OrderingApp.Data.DBConfig;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Connection");

builder.Services.AddDbContext<OrderingDbContext>(options =>
    options.UseSqlServer(connectionString));
var assemblies = Assembly.Load("OrderingApp.Logic");


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
