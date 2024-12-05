//using DataAccess.Repository.IRepository;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Model;
//using Stripe.Checkout;

//namespace Hotel_Track.Controllers
//{
  
//        [Authorize]
//        public class CartController : Controller
//        {
//            private readonly ICartRepository cartRepository;
//            private readonly UserManager<ApplicationUser> userManager;

//            public CartController(ICartRepository cartRepository, UserManager<ApplicationUser> userManager)
//            {
//                this.cartRepository = cartRepository;
//                this.userManager = userManager;
//            }

//            public IActionResult Index()
//            {
//                var ApplicationUserId = userManager.GetUserId(User);

//                var cartProduct = cartRepository.GetAll([e => e.Booking], e => e.ApplicationUserId == ApplicationUserId).ToList();

//                ViewBag.Total = cartProduct.Sum(e => e.Booking.TotalAmount * e.Count);

//                return View(cartProduct.ToList());
//            }
//            public IActionResult AddToCart(int count, int bookingId)
//            {
//                Cart cart = new Cart()
//                {
//                    Count = count,
//                    BookingId = bookingId,
//                    ApplicationUserId = userManager.GetUserId(User)
//                };
//                cartRepository.Add(cart);
//                cartRepository.Commit();
//                TempData["success"] = "Add Booking To Cart success";
//                return RedirectToAction("Index", "Home");
//            }

//            public IActionResult Increment(int bookingId)
//            {
//                var ApplicationUserId = userManager.GetUserId(User);
//                var booking = cartRepository.GetOne(null, e => e.ApplicationUserId == ApplicationUserId && e.BookingId == bookingId);

//                if (booking != null)
//                {
//                    booking.Count++;
//                    cartRepository.Commit();
//                    return RedirectToAction("Index");
//                }
//                return RedirectToAction("NotFoundPage", "Home");
//            }
//            public IActionResult Decrement(int bookingId)
//            {
//                var ApplicationUserId = userManager.GetUserId(User);
//                var booking = cartRepository.GetOne(null, e => e.ApplicationUserId == ApplicationUserId && e.BookingId == bookingId);

//                if (booking != null)
//                {
//                    booking.Count--;
//                    if (booking.Count > 0)
//                        cartRepository.Commit();
//                    else
//                        booking.Count = 1;

//                    return RedirectToAction("Index");
//                }
//                return RedirectToAction("NotFoundPage", "Home");
//            }

//            public IActionResult Delete(int bookingId)
//            {
//                var ApplicationUserId = userManager.GetUserId(User);

//                var booking = cartRepository.GetOne(null, e => e.ApplicationUserId == ApplicationUserId && e.BookingId == bookingId);

//                if (booking != null)
//                {
//                    cartRepository.Delete(booking);
//                    cartRepository.Commit();
//                    return RedirectToAction("Index");
//                }
//                return RedirectToAction("NotFoundPage", "Home");
//            }


//            public IActionResult Pay()
//            {
//                var ApplicationUserId = userManager.GetUserId(User);

//                var cartProduct = cartRepository.GetAll([e => e.Booking], e => e.ApplicationUserId == ApplicationUserId).ToList();

//                var options = new SessionCreateOptions
//                {
//                    PaymentMethodTypes = new List<string> { "card" },
//                    LineItems = new List<SessionLineItemOptions>(),
//                    Mode = "payment",
//                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
//                    CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
//                };

//                foreach (var item in cartProduct)
//                {
//                    options.LineItems.Add(new SessionLineItemOptions
//                    {
//                        PriceData = new SessionLineItemPriceDataOptions
//                        {
//                            Currency = "egp",
//                            ProductData = new SessionLineItemPriceDataProductDataOptions
//                            {
//                                Name = item.Booking.Room.Hotel.Name,
//                            },
//                            UnitAmount = (long)item.Booking.TotalAmount * 100,
//                        },
//                        Quantity = item.Count,
//                    });
//                }

//                var service = new SessionService();
//                var session = service.Create(options);
//                return Redirect(session.Url);
//            }
//        [HttpGet("checkout/success")]
//        public IActionResult Success()
//        {
//            return View();
//        }

//        public IActionResult AllOrder()
//            {
//                var cartProduct = cartRepository.GetAll([e => e.Booking]).ToList();
//                return View(cartProduct.ToList());

//            }
//        }
//}
