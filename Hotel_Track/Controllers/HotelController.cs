
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Hotel_Track.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.ViewModels;
using System.Linq.Expressions;

namespace Hotel_Track.Controllers
{
    [Authorize(Roles = $"{SD.Admin},{SD.Customer}")]
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IRoomRepository roomRepository;
        private readonly IHotelRepository hotelRepository;

        public HotelController(ILogger<HotelController> logger, IRoomRepository roomRepository, IHotelRepository hotelRepository)
        {
            _logger = logger;
            this.roomRepository = roomRepository;
            this.hotelRepository = hotelRepository;
        }
        //List of hotels
        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
                page = 1;
            int pageSize = 3;
            var hotels = hotelRepository.GetAll([e => e.Rooms, e => e.Bookings]).Skip((page - 1) * 3).Take(3).ToList();
            int totalHotels = hotelRepository.GetAll().Count();
            // Calculate total pages
            int totalPages = (int)Math.Ceiling((double)totalHotels / pageSize);
            // Pass hotels and pagination information to the view
            var viewModel = new HotelPaginationViewModel
            {
                Hotels = hotels,
                CurrentPage = page,
                TotalPages = totalPages
            };
            if (hotels.Any())
                return View(viewModel);
            return View("NotFoundPage");
        }

        //Add New Hotel
        public IActionResult Create()
        {
            Hotel hotel = new Hotel();
            ViewBag.HotelType = Enum.GetValues(typeof(EnumHotelType))
         .Cast<EnumHotelType>()
         .Select(e => new { Id = (int)e, Name = e.ToString() })
         .ToList();
            return View(hotel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hotel hotel, IFormFile ImgUrl)
        {
            if (ModelState.IsValid)
            {
                if (ImgUrl.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Hotels", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ImgUrl.CopyTo(stream);
                    }

                    hotel.ImgUrl = fileName;
                }

                hotelRepository.Add(hotel);
                hotelRepository.Commit();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.HotelType = Enum.GetValues(typeof(EnumHotelType))
         .Cast<EnumHotelType>()
         .Select(e => new { Id = (int)e, Name = e.ToString() })
         .ToList();
            return View(hotel);
        }
        //Edit an Hotel
        public IActionResult Edit(int HotelId)
        {
            var hotel = hotelRepository.Find(e => e.Id == HotelId, tracked: false);
            ViewBag.HotelType = Enum.GetValues(typeof(EnumHotelType))
         .Cast<EnumHotelType>()
         .Select(e => new { Id = (int)e, Name = e.ToString() })
         .ToList();
            return View(hotel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hotel hotel, IFormFile ImgUrl)
        {
            var oldHotel = hotelRepository.Find(e => e.Id == hotel.Id, tracked: false);
            if (ModelState.IsValid)
            {
                if (ImgUrl != null && ImgUrl.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Hotels", fileName);
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Hotels", oldHotel.ImgUrl);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ImgUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    hotel.ImgUrl = fileName;
                }
                else
                {
                    hotel.ImgUrl = oldHotel.ImgUrl;
                }
                hotelRepository.Edit(hotel);
                hotelRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.HotelType = Enum.GetValues(typeof(EnumHotelType))
         .Cast<EnumHotelType>()
         .Select(e => new { Id = (int)e, Name = e.ToString() })
         .ToList();
            hotel.ImgUrl = oldHotel.ImgUrl;
            return View(hotel);

        }
        //Delete an hotel
        public IActionResult Delete(int hotelId)
        {
            var hotel = hotelRepository.GetOne(null, e => e.Id == hotelId);

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Hotels", hotel.ImgUrl);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            hotelRepository.Delete(hotel);
            hotelRepository.Commit();

            return RedirectToAction(nameof(Index));
        }

    }
}
