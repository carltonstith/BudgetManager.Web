namespace BudgetManager.Web.Models
{
    public class BillPayment
    {
        public int Id { get; set; }

        public int BillOccurenceId { get; set; }

        public DateOnly PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public BillOccurence BillOccurence { get; set; } = null!;
    }
}