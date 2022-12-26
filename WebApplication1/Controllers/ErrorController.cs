using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        [Route("/Error")]
        public IActionResult Index()
        {
            return View();
        }
    }
}