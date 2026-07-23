using Microsoft.AspNetCore.Mvc;
using RentDriveApi.Model;
using RentDriveApi.Dto;
using RentDriveApi.Interface;
namespace RentDriveApi.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicles()
        {
            var vehicles = await _vehicleRepository.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleById(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Make))
                return BadRequest(new { error = "Vehicle Make is required." });
            if (dto.DailyRate <= 0)
                return BadRequest(new { error = "Daily rate must be greater than zero." });

            var vehicle = new Vehicle
            {
                Make = dto.Make,
                Model = dto.Model,
                DailyRate = dto.DailyRate
            };

            var created = await _vehicleRepository.CreateVehicle(vehicle);
            return CreatedAtAction(nameof(GetVehicleById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleDto dto)
        {
            if (!_vehicleRepository.VehicleExists(id)) return NotFound();

            var vehicle = new Vehicle
            {
                Make = dto.Make,
                Model = dto.Model,
                DailyRate = dto.DailyRate
            };

            var updated = await _vehicleRepository.UpdateVehicle(id, vehicle);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            if (!_vehicleRepository.VehicleExists(id)) return NotFound();
            await _vehicleRepository.DeleteVehicle(id);
            return NoContent();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableVehicles([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate >= endDate)
                return BadRequest(new { error = "Invalid date range." });

            var vehicles = await _vehicleRepository.GetAvailableVehicles(startDate, endDate);
            return Ok(vehicles);
        }
    }
}