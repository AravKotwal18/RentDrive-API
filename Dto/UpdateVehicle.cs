using RentDriveApi.Model;
namespace RentDriveApi.Dto
{
    public class UpdateVehicleDto
    {
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public decimal DailyRate { get; set; }
    }
}