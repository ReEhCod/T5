using Microsoft.EntityFrameworkCore;
using T5.Data;
using T5.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add CORS 
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFrontEnd",
        policy => policy.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});

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

builder.Services.AddScoped<IVehicleRepo, VehicleRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS
app.UseCors("AllowFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
