using ECommerce.Application;
using ECommerce.Infrastructure;
using ECommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use PostgreSQL (from appsettings.json or Render)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseNpgsql(connectionString));
// użyj tylko na Render
if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

var app = builder.Build();

// Wykonaj tylko migrację (bez seedowania testowych danych)
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();

try
{
    dbContext.Database.Migrate();
    Console.WriteLine("Migracje wykonane.");
}
catch (Exception ex)
{
    Console.WriteLine("Błąd podczas migracji: " + ex.Message);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Nie wymuszamy HTTPS (Render robi to sam)
app.UseAuthorization();
app.MapControllers();
app.Run();
