using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(50)]
        public string Make { get; set; } = string.Empty;

        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Range(1900, 2100)]
        public int? Year { get; set; }

        public bool IsActive { get; set; } = true;

        public ApplicationUser User { get; set; } = null!;

        public ICollection<ChargingSession> ChargingSessions { get; set; } = new List<ChargingSession>();
    }
}
