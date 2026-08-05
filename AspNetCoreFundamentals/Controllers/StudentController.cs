using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
