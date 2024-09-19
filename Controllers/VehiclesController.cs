using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T5.Data;
using T5.Models;
using T5.Repositories;

namespace T5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepo _repo;

        public VehiclesController(IVehicleRepo repo)
        {
            _repo = repo;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehiclesAsync()
        {
            return Ok(await _repo.GetAllVehiclesAsync());
        }

        // GET: api/Vehicle/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleByIdAsync(int id)
        {
            var vehicleToGet = await _repo.GetVehicleByIdAsync(id);

            if (vehicleToGet == null) 
            {
                return BadRequest($"The vehicle with id {id} could not be found.");
            }

            return Ok(vehicleToGet);
        }

        // PUT: api/Vehicle/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicleAsync(int id, [FromBody] VehicleModel vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return BadRequest("Ids did not match!");
            }

            var updatedSuccess = await _repo.UpdateVehicleAsync(vehicle);

            if (!updatedSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update the vehicle");
            }

            return NoContent();
        }

        // POST: api/Vehicle
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> AddVehicleAsync([FromBody] VehicleModel vehicle)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var newVehicle = await _repo.AddVehicleAsync(vehicle);

            if (newVehicle == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create the vehicle.");
            }

            return Ok(newVehicle);
            //return CreatedAtAction(nameof(GetVehicleByIdAsync), new { id = newVehicle.VehicleId }, vehicle);

        }

        // DELETE: api/Vehicle/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            var deleteSuccess = await _repo.DeleteVehicleAsync(id);

            if (!deleteSuccess) 
            {
                return NotFound($"The vehicle with {id} was not found.");
            }
            return NoContent();
        }
    }
}
