using System;
namespace RentDriveApi.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Vehicle? Vehicle { get; set; }
    }
}