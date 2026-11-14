using Microsoft.AspNetCore.Identity;

namespace BudgetManager.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        public ICollection<Bill> Bills { get; set; } = new List<Bill>();

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
