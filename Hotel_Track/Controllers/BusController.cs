using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Track.Controllers
{
    public class BusController : Controller
    {
        private readonly IBusRepository busRepository;
        public BusController(IBusRepository busRepository)
        {
            this.busRepository = busRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BusList()
        {
            var Buses = busRepository.GetAll(tracked:false); 
            return View(Buses);
        }

    }
}
