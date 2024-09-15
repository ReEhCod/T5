using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T5.Data;
using T5.Models;

namespace T5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VehicleDbContext _context;

        public VehiclesController(VehicleDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModel>>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModel>> GetVehicleModel(int id)
        {
            var vehicleModel = await _context.Vehicles.FindAsync(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            return vehicleModel;
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicleModel(int id, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.VehicleId)
            {
                return BadRequest();
            }

            _context.Entry(vehicleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VehicleModel>> PostVehicleModel(VehicleModel vehicleModel)
        {
            _context.Vehicles.Add(vehicleModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVehicleModel", new { id = vehicleModel.VehicleId }, vehicleModel);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleModel(int id)
        {
            var vehicleModel = await _context.Vehicles.FindAsync(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicleModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehicleModelExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }
    }
}
