using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Web.Models
{
    public class Bill
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        public decimal ExpectedAmount { get; set; }

        [Range(1, 31)]
        public int DueDay { get; set; }

        public bool IsAutoPay { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public ApplicationUser User { get; set; } = null!;

        public ICollection<BillOccurence> Occurrences { get; set; } = new List<BillOccurence>();
    }
}
