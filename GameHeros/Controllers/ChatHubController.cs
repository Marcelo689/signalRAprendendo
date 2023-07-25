using Microsoft.AspNetCore.Mvc;

namespace GameHeros.Controllers
{
    public class ChatHubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
