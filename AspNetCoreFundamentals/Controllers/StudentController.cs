using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
    public class StudentController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Title = "Student Management System";
            ViewBag.Students = new List<string>
    {
        "Ali Husnain",
        "Ahmed Raza",
        "Usman Ali",
        "Bilal Ahmed"
    };
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
