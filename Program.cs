using Microsoft.EntityFrameworkCore;
using T5.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var T5DbConnectionString = builder.Configuration.GetConnectionString("T5ConnectionString")
    ?? throw new InvalidOperationException("The connection string 'T5ConnectionString' was not found! ");

builder.Services.AddDbContext<VehicleDbContext>(option =>
{
    option.UseSqlServer(T5DbConnectionString);
});

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
