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

// Make the app listen on port 8080 for Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// AUTOMATYCZNE migracje + dodanie danych testowych
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
    dbContext.Database.Migrate();

    // Seed danych testowych (jeśli pusto)
    if (!dbContext.Products.Any())
    {
        dbContext.Products.AddRange(new[]
        {
            new Product
            {
                Name = "Laptop",
                Price = 2999.99,
                Description = "Laptop do pracy i gier",
                IsAvailable = true
            },
            new Product
            {
                Name = "Smartfon",
                Price = 1499.50,
                Description = "Nowoczesny smartfon z dobrym aparatem",
                IsAvailable = true
            }
        });

        dbContext.SaveChanges();
    }
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
