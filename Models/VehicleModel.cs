using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace T5.Models
{
    public class VehicleModel
    {
        public int VehicleId { get; set; }
        public int VIN { get; set; }
        [MaxLength(10)]
        public required string LicensePlateNumber {get; set; }
        public required string Model {  get; set; }
        public required string Brand { get; set; }
    }
}
