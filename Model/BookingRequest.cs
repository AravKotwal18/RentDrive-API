using System;
namespace RentDriveApi.Model
{
    public class BookingRequest
    {
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}