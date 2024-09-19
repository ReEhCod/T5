using Microsoft.EntityFrameworkCore;
using T5.Data;
using T5.Models;

namespace T5.Repositories
{
    public class VehicleRepo : IVehicleRepo
    {
        private readonly VehicleDbContext _dbContext;
        public VehicleRepo(VehicleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<VehicleModel>?> GetAllVehiclesAsync()
        {
            try
            {
                List<VehicleModel> vehicles = await _dbContext.Vehicles
                    .Include(v => v.Equipments)
                    .ToListAsync();

                if (vehicles.Count > 0) 
                { 
                    return vehicles;
                }
                else
                {
                    Console.WriteLine("Might be no vehicles to get");
                    return null;
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Vehicles were not found!", ex.Message);
                return null;
            }
        }

        public async Task<VehicleModel?> GetVehicleByIdAsync(int id)
        {
            try
            {
                VehicleModel? vehicleToGet = await _dbContext.Vehicles
                    .Include(v => v.Equipments)
                    .FirstOrDefaultAsync(v => v.VehicleId == id);

                if (vehicleToGet != null)
                {
                    return vehicleToGet;
                }

                Console.WriteLine($"Vehicle with id {id} was not found");
                return null;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Something went wrong when trying to get vehicle with {id} !", ex.Message);
                return null;
            }
        }

        public async Task<VehicleModel> AddVehicleAsync(VehicleModel vehicle)
        {
            _dbContext.Vehicles.Add(vehicle);

            if (vehicle.Equipments != null)
            {
                foreach (var equipment in vehicle.Equipments)
                {
                    {
                        _dbContext.Equipments.Add(equipment);
                    }
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Failed to add the vehicle to database. Error: " + ex.Message);
                return null;
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Somthing went wrong. Error: " + ex.Message);
            }

            return vehicle;

        }

        public async Task<bool> UpdateVehicleAsync(VehicleModel vehicle)
        {
            try
            {
                var existingVehicle = await _dbContext.Vehicles
                .Include(v => v.Equipments)
                .FirstOrDefaultAsync(v => v.VehicleId == vehicle.VehicleId);

                if (existingVehicle == null)
                {
                    return false;
                }

                // Update vehicle properties
                _dbContext.Entry(existingVehicle).CurrentValues.SetValues(vehicle);

                // update equipments
                var equipmentIds = vehicle.Equipments.Select(e => e.EquipmentId).ToHashSet();
                // Delete old equipments that are no longer present
                var oldEquipments = existingVehicle.Equipments.Where(e => !equipmentIds.Contains(e.EquipmentId)).ToList();

                _dbContext.Equipments.RemoveRange(oldEquipments);

                // Add new equipments or update existing ones
                foreach (var equipment in vehicle.Equipments)
                {
                    var existingEquipment = existingVehicle.Equipments.FirstOrDefault(e => e.EquipmentId == equipment.EquipmentId);

                    if (existingEquipment == null)
                    {                       
                        _dbContext.Equipments.Add(equipment);
                    }
                    else
                    {
                        _dbContext.Entry(existingEquipment).CurrentValues.SetValues(equipment);
                    }
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to update the vehicle" + ex.Message);
                return false;
            }
        }
        
        public async Task<bool> DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicleToDelete = await _dbContext.Vehicles.FindAsync(id);

                if (vehicleToDelete == null)
                {
                    return false;
                }

                _dbContext.Remove(vehicleToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete the vehicle with id {id}." + ex.Message);
                return false;
            }
        }
    }
}
