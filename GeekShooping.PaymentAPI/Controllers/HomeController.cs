using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.PaymentAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
