namespace CircuitShare.Entities
{
    public class PaymentTransaction
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; } // "success", "failed"
        public string FailureMessage { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
