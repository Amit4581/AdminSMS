using Microsoft.AspNetCore.Mvc;

namespace Admin.Controllers
{
    public class PagesController : Controller
    {
        private readonly ILogger<PagesController> _logger;

        public PagesController(ILogger<PagesController> logger)
        {
            _logger = logger;
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult BlogDetails()
        {
            return View();
        }
        public IActionResult CreateBlog()
        {
            return View();
        }

        public IActionResult Chat()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        public IActionResult ContactsUs()
        {
            return View();
        }
        public IActionResult AddProducts()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult EditProducts()
        {
            return View();
        }
        public IActionResult OrderDetails()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Products()
        {
            return View();
        }
        public IActionResult ProductsDetails()
        {
            return View();
        }
        public IActionResult ProductsList()
        {
            return View();
        }

        public IActionResult Wishlist()
        {
            return View();
        }
        public IActionResult Mail()
        {
            return View();
        }
        public IActionResult MailSettings()
        {
            return View();
        }
        public IActionResult EmptyPage()
        {
            return View();
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult FileManager()
        {
            return View();
        }
        public IActionResult CreateInvoice()
        {
            return View();
        }
        public IActionResult InvoiceDetails()
        {
            return View();
        }
        public IActionResult InvoiceList()
        {
            return View();
        }
        public IActionResult Landing()
        {
            return View();
        }
        public IActionResult JobsLanding()
        {
            return View();
        }
        public IActionResult Notifications()
        {
            return View();
        }
        public IActionResult Pricing()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View();
        }
        public IActionResult Terms_conditions()
        {
            return View();
        }
        public IActionResult Timeline()
        {
            return View();
        }

        public IActionResult ToDoList()
        {
            return View();
        }
        

    }
}
