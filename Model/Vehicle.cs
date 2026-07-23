using System;
namespace RentDriveApi.Model
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}