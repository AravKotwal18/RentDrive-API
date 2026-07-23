using RentDriveApi.Model;
namespace RentDriveApi.Interface
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<Vehicle?> GetVehicleById(int id);
        Task<Vehicle> CreateVehicle(Vehicle vehicle);
        Task<Vehicle?> UpdateVehicle(int id, Vehicle vehicle);
        Task<Vehicle?> DeleteVehicle(int id);
        Task<List<Vehicle>> GetAvailableVehicles(DateTime startDate, DateTime endDate);
        bool VehicleExists(int id);
    }
}