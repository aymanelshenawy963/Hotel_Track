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
        public IActionResult Index(int roomId)
        {
            var room = roomRepository.GetOne(null, e => e.Id == roomId);
            var hotelId = room.HotelId;
            var hotel = hotelRepository.GetOne(null, e => e.Id == hotelId);
            ViewData["HotelName"] = hotel.Name;
            return View(room);
        }
        public IActionResult Rooms(int hotelId)
        {
            var rooms = roomRepository.GetAll(inculdeProp: null,expression: r => r.HotelId == hotelId,tracked: false).ToList();
            return View(rooms);
        }
    }
}
