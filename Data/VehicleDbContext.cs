using Microsoft.EntityFrameworkCore;
using T5.Models;

namespace T5.Data
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options) { }

        public DbSet<VehicleModel> Vehicles { get; set; }
        public DbSet<EquipmentModel> Equipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleModel>()
                .HasMany(v => v.Equipments)
                .WithOne()
                .HasForeignKey(e => e.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
