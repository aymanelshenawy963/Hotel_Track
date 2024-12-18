using Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
//using DataAccess.Migrations;

namespace Hotel_Track.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelRepository hotelRepository;

        public HomeController(ILogger<HomeController> logger, IHotelRepository hotelRepository)
        {
            _logger = logger;
            this.hotelRepository = hotelRepository;
        }
        public IActionResult Index(SearchRequest searchRequest, string DateRange, int? hotelId)
        {
            var hotels = hotelRepository.GetAll([e => e.Rooms, e => e.Bookings]).ToList();

            if (!string.IsNullOrEmpty(searchRequest.Destination))
            {
                string search = searchRequest.Destination.Trim().ToLower();
                hotels = hotels.Where(h => h.Location.ToLower().Contains(search) ||
                                      h.Name.ToLower().Contains(search)).ToList();
            }

            if (searchRequest.AdultCapacity > 0)
            {
                hotels = hotels.Where(h => h.Rooms.Any(r => r.AdultCapacity >= searchRequest.AdultCapacity)).ToList();
            }

            if (searchRequest.ChildrenCapacity > 0)
            {
                hotels = hotels.Where(h => h.Rooms.Any(r => r.ChildrenCapacity >= searchRequest.ChildrenCapacity)).ToList();
            }

            if (searchRequest.PetsAllowed)
            {
                hotels = hotels.Where(e => e.PetsAllowed == searchRequest.PetsAllowed).ToList();
            }

            if (searchRequest.MaxPrice > 0)
            {
                hotels = hotels.Where(h => h.MaxPrice <= searchRequest.MaxPrice).ToList();
            }

            if (!string.IsNullOrEmpty(searchRequest.Amenities))
            {
                hotels = hotels.Where(h => h.Amenities.Contains(searchRequest.Amenities)).ToList();
            }

            if (!string.IsNullOrEmpty(DateRange) && DateRange.Contains(" to "))
            {
                var dates = DateRange.Split(" to ");
                if (dates.Length == 2)
                {
                    if (DateTime.TryParse(dates[0], out DateTime startDate) && DateTime.TryParse(dates[1], out DateTime endDate))
                    {
                        hotels = hotels.Where(h => h.Bookings.Any(b => b.CheckInDate >= searchRequest.StartDate && b.CheckInDate <= searchRequest.EndDate)).ToList();
                    }
                }
            }
            if (searchRequest.HotelType != null && searchRequest.HotelType.Any())
            {
                hotels = hotels.Where(h => searchRequest.HotelType.Contains((int)h.HotelType)).ToList();
            }
            if (hotelId.HasValue)
            {
                var hotel = hotels.FirstOrDefault(h => h.Id == hotelId.Value);
                if (hotel != null)
                {
                    var availableRoom = hotel.Rooms.FirstOrDefault(r => r.IsAvailable);
                    if (availableRoom != null)
                    {
                        TempData["SuccessMessage"] = "The hotel is available!";
                        return RedirectToAction("Details", new { hotelId = hotel.Id });

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "No rooms are available at the moment.";
                    }
                }
            }
            if (hotels == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            return View(hotels.Take(6).ToList());
        }

        public IActionResult Details(int hotelId)
        {
            var hotel = hotelRepository.GetAll([e => e.Rooms, e => e.Bookings]) 
                .FirstOrDefault(h => h.Id == hotelId);

            if (hotel == null)
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
            foreach (var room in hotel.Rooms)
            {
                room.IsAvailable = !room.Bookings.Any(b => b.CheckInDate <= DateTime.Now && b.CheckOutDate >= DateTime.Now); 
            }
            return View(hotel);
        }
        public IActionResult NotFoundPage()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}