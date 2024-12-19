using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Model.ViewModels;

namespace Hotel_Track.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender emailSender;
        public ContactController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            this.emailSender = emailSender;
        }
        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string subject = $"Contact Us Message: {model.Subject}";
                string body = $"Name: {model.Name}<br>Email: {model.Email}<br>Message: {model.Message}";
                await emailSender.SendEmailAsync("sa3a.7anan@gmail.com", subject, body);
                ViewBag.Message = "Thank you for contacting us. We will get back to you soon.";
                return View(new ContactViewModel());
            }

            return View(model);
        }

    }
}
