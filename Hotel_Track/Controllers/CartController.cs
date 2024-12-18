using DataAccess.IRepository;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;
using Stripe.Checkout;

namespace Hotel_Track.Controllers
{

    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBookingRepository bookingRepository;

        public CartController(ICartRepository cartRepository, UserManager<ApplicationUser> userManager, IBookingRepository bookingRepository)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.bookingRepository = bookingRepository;
        }

        public IActionResult Index()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var book = bookingRepository.GetAll([e=>e.Room,e=>e.Hotel], e => e.ApplicationUserId == ApplicationUserId).ToList();
            ViewBag.Total = book.Sum(e => e.Room.PricePerNight*e.Count );

            return View(book.ToList());
        }
        [HttpPost]
        public IActionResult AddToCart(Booking booking, int roomId, int hotelId, DateTime? checkInDate, DateTime? checkOutDate)
        {
            if (roomId <= 0 || hotelId <= 0 || !checkInDate.HasValue || !checkOutDate.HasValue)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            if (checkInDate >= checkOutDate)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            int numberOfDays = (checkOutDate.Value - checkInDate.Value).Days;

            if (numberOfDays <= 0)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            // Check for overlapping bookings
            var existingBooking = bookingRepository.GetAll()
                .FirstOrDefault(b => b.RoomId == roomId &&
                                     b.HotelId == hotelId &&
                                     ((checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate) || // Overlaps at the start
                                      (checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate) || // Overlaps at the end
                                      (checkInDate <= b.CheckInDate && checkOutDate >= b.CheckOutDate))); // Encompasses an existing booking

            if (existingBooking != null)
            {
                TempData["Success"] = "The selected room is already booked for the specified dates.";
                return RedirectToAction("Index", "Cart");
            }

            // Proceed with booking
            booking = new Booking
            {
                CheckInDate = checkInDate.Value,
                CheckOutDate = checkOutDate.Value,
                ApplicationUserId = userManager.GetUserId(User),
                RoomId = roomId,
                HotelId = hotelId,
                Count = numberOfDays
            };

            bookingRepository.Add(booking);
            bookingRepository.Commit();
            TempData["success"] = $"Booking added to cart successfully for {numberOfDays} days.";
            return RedirectToAction("Index", "Cart");
        }



        public IActionResult Delete(int bookingId)
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var booking = bookingRepository.GetOne(expression: e => e.ApplicationUserId == ApplicationUserId && e.Id == bookingId);

            if (booking != null)
            {
                bookingRepository.Delete(booking);
                bookingRepository.Commit();
                return RedirectToAction("Index");
            }
            return RedirectToAction("NotFoundPage", "Home");
        }
        public IActionResult Pay()
        {
            var ApplicationUserId = userManager.GetUserId(User);

            var bookings = bookingRepository.GetAll([e => e.Room,e=>e.Hotel], e => e.ApplicationUserId == ApplicationUserId).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            foreach (var item in bookings)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Room.Hotel.Name,
                        },
                        UnitAmount = (long)item.Room.PricePerNight * 100,
                    },
                    Quantity = item.Count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
        [HttpGet("checkout/success")]
        public IActionResult Success()
        {
            return View();
        }

        public IActionResult AllOrder()
        {
            var bookings = bookingRepository.GetAll();
            return View(bookings.ToList());
        }
    }
}
