using Microsoft.EntityFrameworkCore;
using T5.Models;

namespace T5.Data
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options) { }

        public DbSet<VehicleModel> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleModel>(entity =>
            {
                entity.HasKey(v => v.VehicleId);
            });
        }
    }
}
