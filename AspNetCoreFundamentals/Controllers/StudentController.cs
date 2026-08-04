using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return Content("Student Controller Working");
        }

        public IActionResult Details()
        {
            return Content("Student Details");
        }
    }
}
