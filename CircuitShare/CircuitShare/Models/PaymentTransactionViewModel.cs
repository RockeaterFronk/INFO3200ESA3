using System.ComponentModel.DataAnnotations;

namespace CircuitShare.Models
{
    public class PaymentTransactionViewModel
    {
        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        public string ExpiryDate { get; set; }

        [Required]
        [MinLength(3)]
        public string CVV { get; set; }

        [Required]
        [Range(0.01, 9999.99)]  // Make sure the payment is greater than zero
        public decimal Amount { get; set; }
    }

}
