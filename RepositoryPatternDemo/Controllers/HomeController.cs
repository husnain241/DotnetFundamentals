using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using System.Diagnostics;

namespace RepositoryPatternDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
