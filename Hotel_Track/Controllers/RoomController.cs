using DataAccess.IRepository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;

namespace Hotel_Track.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository roomRepository;
        private readonly IHotelRepository hotelRepository;
        private readonly IBookingRepository bookingRepository;

        public RoomController(IRoomRepository roomRepository, IHotelRepository hotelRepository,IBookingRepository bookingRepository)
        {
            this.roomRepository = roomRepository;
            this.hotelRepository = hotelRepository;
            this.bookingRepository = bookingRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index(int hotelId, int page = 1)
        {
            int pageSize = 15; 
            var allRooms = roomRepository.GetAll(); 
            int totalRooms = allRooms.Count();

           
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRooms / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.room = ViewBag.TotalPages;

            if (page <= 0)
                page = 1;

            var rooms = allRooms.Skip((page - 1) * pageSize).Take(pageSize);

            if (rooms.Any())
            {
                return View(rooms.ToList());
            }
            return RedirectToAction("NotFoundPage", "Home");
        }

        public IActionResult Rooms(int hotelId )
        {
                var rooms = roomRepository.GetAll( expression: e => e.HotelId == hotelId, tracked: false).ToList();
                return View(rooms);
        }
        public IActionResult Details(int roomId)
        {
            var room = roomRepository.GetOne([e=>e.Hotel], e => e.Id == roomId);
            ViewData["HotelName"] = room.Hotel.Name;
            //var reservedDates = bookingRepository.GetAll(expression: b => b.RoomId == roomId && b.HotelId == room.HotelId).Select(b => b.CheckInDate.Date).ToList();    
            //ViewData["ReservedDates"] = reservedDates;
            return View(room);
        }
 


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
             Room room = new Room();
            ViewBag.hotelId = new SelectList(hotelRepository.GetAll(), "Id", "Name");
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room, IFormFile ImgUrl)
        {
            
            if (ModelState.IsValid)
            {
                if (ImgUrl != null && ImgUrl.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Rooms", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ImgUrl.CopyTo(stream);
                    }
                    room.ImgUrl = fileName;
                }
                roomRepository.Add(room);
                roomRepository.Commit();

                return RedirectToAction(nameof(Index));


            }
            return View(room);
        }



        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int roomId)
        {
            ViewBag.hotelId = new SelectList(hotelRepository.GetAll( tracked: false), "Id", "Name");

            var room = roomRepository.GetOne(null, x => x.Id == roomId, tracked: false);
            return View(room);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Room room, IFormFile ImgUrl)
        {
            var oldFile = roomRepository.GetOne(null, x => x.Id == room.Id, tracked: false);
            //ModelState.Remove("ImgUrl");

            if (ModelState.IsValid)
            {

                if (ImgUrl != null && ImgUrl.Length > 0)//99656
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUrl.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Rooms", fileName);
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\Rooms", oldFile.ImgUrl);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        ImgUrl.CopyTo(stream);
                    }

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                    room.ImgUrl = fileName;
                }

                else
                {
                    room.ImgUrl = room.ImgUrl;
                }
                roomRepository.Edit(room);
                roomRepository.Commit();
                return RedirectToAction(nameof(Index));

            }
            return View(room);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int roomId)
        {
            Room room = new Room() { Id = roomId };
            roomRepository.Delete(room);
            roomRepository.Commit();
            return RedirectToAction(nameof(Index));

        }




    }
}
