using Microsoft.EntityFrameworkCore;
using RentDriveApi.Model;
using RentDriveApi.Data;
using RentDriveApi.Interface;
namespace RentDriveApi.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DataContext _context;
        public VehicleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }
        public async Task<Vehicle?> GetVehicleById(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }
        public async Task<Vehicle> CreateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
            return vehicle;
        }
        public async Task<Vehicle?> UpdateVehicle(int id, Vehicle vehicle)
        {
            var existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle != null)
            {
                existingVehicle.Make = vehicle.Make;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.DailyRate = vehicle.DailyRate;
                await _context.SaveChangesAsync();
            }
            return existingVehicle;
        }
        public async Task<Vehicle?> DeleteVehicle(int id)
        {
            var existingVehicle = await _context.Vehicles.FindAsync(id);
            if (existingVehicle != null)
            {
                _context.Vehicles.Remove(existingVehicle);
                await _context.SaveChangesAsync();
            }
            return existingVehicle;
        }
        public async Task<List<Vehicle>> GetAvailableVehicles(DateTime startDate, DateTime endDate)
        {
            return await _context.Vehicles
                .Where(v => !v.Bookings.Any(b => (startDate <= b.EndDate && endDate >= b.StartDate)))
                .ToListAsync();
        }
        public bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}