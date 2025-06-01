using ECommerce.Application;
using ECommerce.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use PostgreSQL (not SQL Server anymore)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseNpgsql(connectionString)); // <-- Używamy PostgreSQL

// Make the app listen on port 8080 for Render
builder.WebHost.UseUrls("http://0.0.0.0:8080");

var app = builder.Build();

// AUTOMATYCZNE migracje bazy przy starcie aplikacji
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
    dbContext.Database.Migrate();
}

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
