namespace BudgetManager.Web.Models
{
    public class BillOccurence
    {
        public int Id { get; set; }

        public int BillId { get; set; }

        public DateOnly BillingMonth { get; set; }

        public DateOnly DueDate { get; set; }

        public decimal AmountDue { get; set; }

        public Bill Bill { get; set; } = null!;

        public ICollection<BillPayment> Payments { get; set; } = new List<BillPayment>();
    }
}