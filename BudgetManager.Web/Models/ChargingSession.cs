using BudgetManager.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models
{
    public class ChargingSession
    {
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public DateTime ChargingDate { get; set; }

        public ChargingType ChargingType { get; set; }

        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        public decimal EnergyKWh { get; set; }

        public decimal PricePerKWh { get; set; }

        public decimal TotalCost { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public Vehicle Vehicle { get; set; } = null!;
    }
}