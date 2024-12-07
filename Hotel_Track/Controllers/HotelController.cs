using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq.Expressions;

namespace Hotel_Track.Controllers
{
    public class HotelController : Controller
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IRoomRepository roomRepository;
        private readonly IHotelRepository hotelRepository;

        public HotelController(ILogger<HotelController> logger, IRoomRepository roomRepository , IHotelRepository hotelRepository)
        {
            _logger = logger;
            this.roomRepository = roomRepository;
            this.hotelRepository = hotelRepository;
        }
        //List of hotels
        public IActionResult Index()
        {
            var hotels = hotelRepository.GetAll().ToList();
            return View(hotels); 
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

            return View(hotel);
        }

        public IActionResult Edit(int HotelId)
        {
            var hotel = hotelRepository.GetOne(null, e => e.Id == HotelId);
            ViewBag.HotelType = Enum.GetValues(typeof(EnumHotelType))
         .Cast<EnumHotelType>()
         .Select(e => new { Id = (int)e, Name = e.ToString() })
         .ToList();
            return View(hotel);
        }
        [HttpPost]
        public IActionResult Edit(Hotel hotel, IFormFile ImgUrl)
        {
            var oldHotel = hotelRepository.GetOne(null,e => e.Id == hotel.Id);
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

        public IActionResult Delete(int hotelId)
        {
            var hotel = hotelRepository.GetOne(null, e => e.Id == hotelId);

            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Hotels",hotel.ImgUrl);

            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }

            hotelRepository.Delete(hotel);
            hotelRepository.Commit();

            return RedirectToAction(nameof(Index));
        }
        //ٌget one room of an hotel rooms
        public IActionResult Room(int roomId)
        {
            var room = roomRepository.GetOne(null, e => e.Id == roomId);
            var hotelId = room.HotelId;
            var hotel = hotelRepository.GetOne(null, e => e.Id == hotelId);
            ViewData["HotelName"] = hotel.Name;
            return View(room);
        }
        //get all rooms in one hotel
        public IActionResult Rooms(int hotelId)
        {
            var rooms = roomRepository.GetAll(inculdeProp: null,expression: r => r.HotelId == hotelId,tracked: false).ToList();
            return View(rooms);
        }
    }
}
