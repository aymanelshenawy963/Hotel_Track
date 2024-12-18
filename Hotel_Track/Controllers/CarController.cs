using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.Linq.Expressions;

namespace Hotel_Track.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository carRepository;
        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        public IActionResult Index(SearchRequest searchRequest)
        {
 
            return View();
        }
        public IActionResult Search(SearchRequest searchRequest)
        {
            var cars = carRepository.GetAll();
            if (!string.IsNullOrEmpty(searchRequest.DepartureLocation))
            {
                cars = cars.Where(c => c.Location.ToLower().Trim().Equals(searchRequest.DepartureLocation.ToLower().Trim()));
                return View(cars.ToList());
            }
            if (searchRequest.StartDate !=null)
            {
                cars = cars.Where(c => c.IsAvailable);
                return View(cars.ToList());
            }
            return View(cars.ToList());
        }
        public IActionResult CarList()
        {
            var cars = carRepository.GetAll();
            return View(cars);
        }
        public IActionResult Create()
        {
            Car car = new Car();
            return View(car);
        }
        [HttpPost]
        public IActionResult Create(Car car)
        {
            if(ModelState.IsValid)
            {
                carRepository.Add(car);
                carRepository.Commit();
                return RedirectToAction("CarList");
            }
            return View(car);
        }

        public IActionResult Edit(int carId)
        {
            var category = carRepository.GetOne(expression:e => e.Id == carId);

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                carRepository.Edit(car);
                carRepository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }
    }
}
