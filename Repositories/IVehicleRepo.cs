using T5.Models;

namespace T5.Repositories
{
    public interface IVehicleRepo
    {
        Task<List<VehicleModel>?> GetAllVehiclesAsync();
        Task<VehicleModel?> GetVehicleByIdAsync(int id);
        Task<VehicleModel> AddVehicleAsync(VehicleModel vehicle);
        Task<bool> UpdateVehicleAsync(VehicleModel vehicle);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
