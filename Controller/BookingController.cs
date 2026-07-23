using Microsoft.AspNetCore.Mvc;
using RentDriveApi.Model;
using RentDriveApi.Interface;
namespace RentDriveApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public BookingController(IBookingRepository bookingRepository, IVehicleRepository vehicleRepository)
        {
            _bookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequest request)
        {
            if (request.StartDate.Date < DateTime.Today)
                return BadRequest(new { error = "Start date cannot be in the past." });

            if (request.StartDate >= request.EndDate)
                return BadRequest(new { error = "End date must be strictly after the start date." });

            if (!_vehicleRepository.VehicleExists(request.VehicleId))
                return NotFound(new { error = "Vehicle not found." });

            var hasOverlap = await _bookingRepository.HasOverlappingBooking(request.VehicleId, request.StartDate, request.EndDate);
            if (hasOverlap)
                return Conflict(new { error = "Vehicle is already booked for these dates." });

            var vehicle = await _vehicleRepository.GetVehicleById(request.VehicleId);
            if (vehicle == null)
                return NotFound(new { error = "Vehicle not found." });
            int days = (request.EndDate.Date - request.StartDate.Date).Days;
            decimal totalPrice = days * vehicle.DailyRate;

            var booking = new Booking
            {
                VehicleId = request.VehicleId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                TotalPrice = totalPrice
            };

            var created = await _bookingRepository.CreateBooking(booking);
            return CreatedAtAction(nameof(GetBookingsByVehicle), new { vehicleId = created.VehicleId }, created);
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetBookingsByVehicle(int vehicleId)
        {
            var bookings = await _bookingRepository.GetBookingsByVehicle(vehicleId);
            return Ok(bookings);
        }

        [HttpDelete("{vehicleId}/{startDate}/{endDate}")]
        public async Task<IActionResult> DeleteBooking(int vehicleId, DateTime startDate, DateTime endDate)
        {
            var deleted = await _bookingRepository.DeleteBooking(vehicleId, startDate, endDate);
            if (!deleted)
                return NotFound(new { error = "Enrollment record not found." });
            return NoContent();
        }
    }
}