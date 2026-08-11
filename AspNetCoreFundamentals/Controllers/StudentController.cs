using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
    public class StudentController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Title = "Student Management System";
            ViewBag.Students = new Dictionary<int, string>
    {
        { 1, "Ali Husnain" },
        { 2, "Ahmed Raza" },
        { 3, "Usman Ali" },
        { 4, "Bilal Ahmed" }
    };
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
