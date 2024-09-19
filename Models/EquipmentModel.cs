using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T5.Models
{
    public class EquipmentModel
    {
        [Key]
        public int EquipmentId { get; set; }
        [ForeignKey(nameof(VehicleId))]
        public int VehicleId { get; set; }
        public required string EquipmentName { get; set; }
        public bool IsEquipped { get; set; }
        public bool IsWorking { get; set; }
    }
}
