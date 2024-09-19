using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace T5.Models
{
    public class VehicleModel
    {
        [Key]
        public int VehicleId { get; set; }
        public required string VIN { get; set; }
        [MaxLength(10)]
        public required string LicensePlateNumber {get; set; }
        public required string Model {  get; set; }
        public required string Brand { get; set; }

        public List<EquipmentModel> Equipments { get; set; } = new List<EquipmentModel>();
    }
}
