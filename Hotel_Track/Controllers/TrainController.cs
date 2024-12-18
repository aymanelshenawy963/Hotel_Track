using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Track.Controllers
{
    public class TrainController : Controller
    {
        private readonly ITrainRepository trainRepository;
        public TrainController(ITrainRepository trainRepository)
        {
            this.trainRepository = trainRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
