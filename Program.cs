using Microsoft.EntityFrameworkCore;
using RentDriveApi.Data;
using RentDriveApi.Interface;
using RentDriveApi.Repository;
using RentDriveApi.Model;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

// Seed Default Vehicles if none exist
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    if (!context.Vehicles.Any())
    {
        context.Vehicles.AddRange(new List<Vehicle>
        {
            new Vehicle { Make = "Toyota", Model = "Prius (RentDrive Go)", DailyRate = 35.00m },
            new Vehicle { Make = "Tesla", Model = "Model Y (RentDrive EV)", DailyRate = 65.00m },
            new Vehicle { Make = "Chevrolet", Model = "Suburban (RentDrive XL)", DailyRate = 95.00m },
            new Vehicle { Make = "BMW", Model = "M5 (RentDrive Lux)", DailyRate = 145.00m }
        });
        context.SaveChanges();
    }
}
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();