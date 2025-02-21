using CircuitShare.Entities;

namespace CircuitShare.Controllers
{
    using CircuitShare.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class PaymentController : Controller
    {
        private readonly CircuitShareDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PaymentController(CircuitShareDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fix the errors in the form.";
                return RedirectToAction("Index");
            }

            var paymentResponse = SimulatePaymentGatewayResponse(model);

            var user = await _userManager.GetUserAsync(User);
            var transaction = new PaymentTransaction
            {
                TransactionId = paymentResponse.TransactionId ?? Guid.NewGuid().ToString(),
                UserId = user.Id,
                Amount = model.Amount,
                PaymentMethod = "Credit Card",  // Assuming that Credit card is the only payment method for now
                Status = paymentResponse.Status,
                FailureMessage = paymentResponse.Status == "failed" ? paymentResponse.Message : null,
                CreatedAt = DateTime.UtcNow
            };

            _context.PaymentTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            if (paymentResponse.Status == "success")
            {
                TempData["SuccessMessage"] = "Payment was successful!";
            }
            else
            {
                TempData["ErrorMessage"] = "Payment failed: " + paymentResponse.Message;
            }

            return RedirectToAction("Index");
        }

        private PaymentResponse SimulatePaymentGatewayResponse(PaymentTransactionViewModel model)
        {
            // Assume the user has $10 available for payment
            decimal availableFunds = 10m;

            var response = new PaymentResponse
            {
                Status = "success",
                TransactionId = Guid.NewGuid().ToString(),
                Message = "Payment successful"
            };

            // Simulate payment failure if the requested amount exceeds available funds
            if (model.Amount > availableFunds)
            {
                response.Status = "failed";
                response.Message = $"Insufficient funds. You only have ${availableFunds} available.";
            }

            return response;
        }

    }

    public class PaymentResponse
    {
        public string Status { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }
    }
}
