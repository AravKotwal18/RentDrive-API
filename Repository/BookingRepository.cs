using Microsoft.EntityFrameworkCore;
using RentDriveApi.Model;
using RentDriveApi.Data;
using RentDriveApi.Interface;
namespace RentDriveApi.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        public BookingRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Booking> CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }
        public async Task<List<Booking>> GetBookingsByVehicle(int vehicleId)
        {
            return await _context.Bookings.Where(b => b.VehicleId == vehicleId).ToListAsync();
        }
        public async Task<bool> HasOverlappingBooking(int vehicleId, DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings.AnyAsync(b => b.VehicleId == vehicleId && (startDate <= b.EndDate && endDate >= b.StartDate));
        }
        public async Task<Booking?> GetBooking(int vehicleId, DateTime startDate, DateTime endDate)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.VehicleId == vehicleId && (startDate <= b.EndDate && endDate >= b.StartDate));
        }
        public async Task<bool> DeleteBooking(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var existingBooking = await _context.Bookings.FirstOrDefaultAsync(b => b.VehicleId == vehicleId && (startDate <= b.EndDate && endDate >= b.StartDate));
            if (existingBooking != null)
            {
                _context.Bookings.Remove(existingBooking);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}