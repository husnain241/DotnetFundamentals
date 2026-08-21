using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
