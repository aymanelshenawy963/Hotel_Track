using Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Track.Controllers
{
    //[Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelRepository hotelRepository;

        public HomeController(ILogger<HomeController> logger,IHotelRepository hotelRepository)
        {
            _logger = logger;
            this.hotelRepository = hotelRepository;
        }

        public async Task<IActionResult> Index(string? search = null)
        {
            var hotelsQuery = hotelRepository.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                hotelsQuery = hotelsQuery.Where(e => e.Name.ToLower().Contains(search.ToLower()));
            }

            var hotels = await hotelsQuery.ToListAsync();

            return View(hotels);
        }
        public async Task<IActionResult> Details(int hotelId)
        {
            var hotel = hotelRepository.GetOne(null,e => e.Id == hotelId);

            if (hotel != null)
            {
                return View( hotel);
            }
            else
            {
                return RedirectToAction(nameof(NotFoundPage));
            }
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
