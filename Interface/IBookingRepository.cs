using RentDriveApi.Model;

namespace RentDriveApi.Interface
{
    public interface IBookingRepository
    {
        Task<Booking> CreateBooking(Booking booking);
        Task<List<Booking>> GetBookingsByVehicle(int vehicleId);
        Task<bool> HasOverlappingBooking(int vehicleId, DateTime startDate, DateTime endDate);
        Task<Booking?> GetBooking(int vehicleId, DateTime startDate, DateTime endDate);
        Task<bool> DeleteBooking(int vehicleId, DateTime startDate, DateTime endDate);
        bool BookingExists(int id);
    }
}