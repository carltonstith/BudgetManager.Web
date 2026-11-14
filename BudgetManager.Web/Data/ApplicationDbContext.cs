using BudgetManager.Web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Bill> Bills => Set<Bill>();
        public DbSet<BillOccurence> BillOccurences => Set<BillOccurence>();
        public DbSet<BillPayment> BillPayments => Set<BillPayment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Category)
                    .HasMaxLength(100);

                entity.Property(e => e.ExpectedAmount)
                    .HasPrecision(18, 2);

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Bills)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<BillOccurence>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.AmountDue)
                    .HasPrecision(18, 2);
                
                entity.HasOne(e => e.Bill)
                    .WithMany(b => b.Occurrences)
                    .HasForeignKey(e => e.BillId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new 
                { 
                    e.BillId, 
                    e.BillingMonth 
                }).IsUnique();
            });

            builder.Entity<BillPayment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Amount)
                    .HasPrecision(18, 2);

                entity.HasOne(e => e.BillOccurence)
                    .WithMany(o => o.Payments)
                    .HasForeignKey(e => e.BillOccurenceId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Make)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Year)
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Vehicles)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(e => e.UserId);
            });

            builder.Entity<ChargingSession>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.EnergyKWh)
                    .HasPrecision(12, 3);

                entity.Property(e => e.PricePerKWh)
                    .HasPrecision(10, 4);

                entity.Property(e => e.TotalCost)
                    .HasPrecision(18, 2);

                entity.Property(e => e.Location)
                    .HasMaxLength(150);

                entity.Property(e => e.Notes)
                    .HasMaxLength(500);

                entity.Property(e => e.ChargingType)
                    .HasConversion<int>();

                entity.HasOne(e => e.Vehicle)
                    .WithMany(v => v.ChargingSessions)
                    .HasForeignKey(e => e.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.VehicleId, e.ChargingDate });
            });
        }
    }
}
