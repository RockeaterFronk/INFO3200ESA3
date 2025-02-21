using CircuitShare.Entities;
using CircuitShare.Models;
using CircuitShare.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CircuitShare.Controllers
{
    public class ChargerController : Controller
    {
        private CircuitShareDbContext _circuitShareDbContext { get; set; }
        private ChargerManager _chargerManager;

        public ChargerController(CircuitShareDbContext circuitShareDbContext, ChargerManager chargerManager)
        {
            _circuitShareDbContext = circuitShareDbContext;
            _chargerManager = chargerManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Chargers")]
        public IActionResult GetAllChargers()
        {
            var allChargers = _chargerManager.GetChargers();

            ChargerViewModel chargerViewModel = new ChargerViewModel()
            {
                Chargers = allChargers,
            };

            return View("Chargers", chargerViewModel);
        }

        // GET: Chargers/GetChargerAsync/5
        //[Route("Charger/GetChargerAsync/{id}")]
        //public async Task<IActionResult> GetChargerAsync(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var charger = await _circuitShareDbContext.Chargers
        //        .FirstOrDefaultAsync(c => c.ChargerId == id);

        //    if (charger == null)
        //    {
        //        return NotFound();
        //    }

        //    await _circuitShareDbContext.SaveChangesAsync();

        //    return View("ChargerDetails", charger);
        //}

        /// <summary>
        /// Creates a new add request
        /// </summary>
        /// <returns> A new add form using with a new chargerviewmodel</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet("/Chargers/add-request")]
        public IActionResult GetAddChargerRequest()
        {
            Charger chargerViewModel = new Charger()
            {
                HourlyRate = 0
            };

            return View("AddCharger", chargerViewModel);
        }

        [HttpPost("/Chargers")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewCharger(Charger charger)
        {
            if (ModelState.IsValid)
            {
                _chargerManager.AddNewCharger(charger);
                await _circuitShareDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(GetAllChargers));
            }

            return View("AddCharger", charger);
        }

        // GET: Chargers/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditCharger(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charger = await _circuitShareDbContext.Chargers.FindAsync(id);

            if (charger == null)
            {
                return NotFound();
            }
            return View(charger);
        }

        // POST: Chargers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCharger(int id, [Bind("ChargerId,Name,Description,HourlyRate")] Charger charger)
        {
            if (id != charger.ChargerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _circuitShareDbContext.Update(charger);
                    await _circuitShareDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChargerExists(charger.ChargerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetAdminPanel", "Account");
            }
            return View(charger);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCharger(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var charger = await _circuitShareDbContext.Chargers
                .FirstOrDefaultAsync(c => c.ChargerId == id);
            if (charger == null)
            {
                return NotFound();
            }

            return View(charger);
        }

        [HttpPost, ActionName("DeleteCharger")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteChargerConfirmed(int id)
        {
            var charger = await _circuitShareDbContext.Chargers.FindAsync(id);
            if (charger != null)
            {
                _circuitShareDbContext.Chargers.Remove(charger);
            }

            await _circuitShareDbContext.SaveChangesAsync();
            return RedirectToAction("GetAdminPanel", "Account");
        }

        private bool ChargerExists(int id)
        {
            return _circuitShareDbContext.Chargers.Any(e => e.ChargerId == id);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChargerRental(int chargerId)
        {
            var charger = _circuitShareDbContext.Chargers
                .FirstOrDefault(c => c.ChargerId == chargerId);

            if (charger == null)
            {
                return NotFound();
            }

            var viewModel = new ChargerRentalViewModel
            {
                Charger = charger,
                IsRented = charger.RentalStartTime.HasValue && charger.RentalEndTime == null,
                IsUserRenting = charger.RentedByUserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            return View(viewModel);
        }



        [HttpPost]
        public IActionResult StartRental(int chargerId)
        {
            var charger = _circuitShareDbContext.Chargers
                .FirstOrDefault(c => c.ChargerId == chargerId);

            if (charger != null)
            {
                charger.RentalStartTime = DateTime.UtcNow;

                charger.RentedByUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _circuitShareDbContext.SaveChanges();
            }

            return RedirectToAction("ChargerRental", new { chargerId = chargerId });
        }



        [HttpPost]
        public IActionResult StopRental(int chargerId)
        {
            var charger = _circuitShareDbContext.Chargers.FirstOrDefault(c => c.ChargerId == chargerId);

            if (charger != null && charger.RentalStartTime.HasValue)
            {
                charger.RentalEndTime = null;
                charger.RentedByUserId = null;

                _circuitShareDbContext.SaveChanges();
            }

            return RedirectToAction("ChargerRental", new { chargerId = chargerId });
        }


        [HttpGet]
        public IActionResult GetRentalStatus(int chargerId)
        {
            var charger = _circuitShareDbContext.Chargers.FirstOrDefault(c => c.ChargerId == chargerId);

            if (charger != null && charger.RentalStartTime.HasValue)
            {
                var elapsedTime = DateTime.UtcNow - charger.RentalStartTime.Value;

                charger.TotalElapsedTime += elapsedTime.TotalSeconds;

                charger.AmountDue = Math.Round((charger.TotalElapsedTime / 3600) * charger.HourlyRate, 2);

                return Json(new { success = true, elapsedTime = elapsedTime.TotalSeconds, amountDue = charger.AmountDue });
            }

            return Json(new { success = false });
        }


        //public IActionResult Checkout(decimal amountDue)
        //{
        //    var user = User.Identity.Name;
        //    var paymentMethod = _circuitShareDbContext.PaymentMethods.FirstOrDefault(p => p.UserId == user);

        //    if (paymentMethod == null)
        //    {
        //        // Redirect to add payment method if none exists
        //        return RedirectToAction("AddPaymentMethod", new { returnUrl = Url.Action("Checkout", new { amountDue = amountDue }) });
        //    }

        //    // If payment method exists, proceed with the payment
        //    return View(new CheckoutViewModel { AmountDue = amountDue });
        //}


        //Just a method for helping me debug the timers
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult StopAllRentals()
        {
            var rentedChargers = _circuitShareDbContext.Chargers
                .ToList();

            foreach (var charger in rentedChargers)
            {
                charger.TotalElapsedTime = 0;
                charger.RentalEndTime = null;
                charger.RentalStartTime = null;
                charger.RentedByUserId = null;
                charger.AmountDue = 0;
                _circuitShareDbContext.SaveChanges();
            }

            return RedirectToAction("GetAdminPanel", "Account");
        }

    }
}
