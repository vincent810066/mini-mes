using Microsoft.EntityFrameworkCore;
using MiniMES.Models;

namespace MiniMES.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<ProductionRecord> ProductionRecords { get; set; }
        public DbSet<AlertRecord> AlertRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Equipment>()
                .HasIndex(e => e.EquipmentCode)
                .IsUnique();

            modelBuilder.Entity<WorkOrder>()
                .HasIndex(w => w.WorkOrderNumber)
                .IsUnique();
        }
    }
}