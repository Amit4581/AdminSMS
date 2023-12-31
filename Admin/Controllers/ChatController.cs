using Admin.Contract.Interface.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Web.Controllers
{
    public class ChatController : Controller
    {

        private readonly IUserRepositry _userDataService;

        public ChatController(IUserRepositry userDataService)
        {
            _userDataService = userDataService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
