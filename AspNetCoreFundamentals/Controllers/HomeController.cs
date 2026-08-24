using AspNetCoreFundamentals.Configuration;
using AspNetCoreFundamentals.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AspNetCoreFundamentals.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationSettings _settings;


        public HomeController(IConfiguration configuration, IOptions<ApplicationSettings> options)
        {
            _configuration = configuration;
            _settings = options.Value;
        }

        public IActionResult Index()
        {
            var appName = _configuration["ApplicationSettings:ApplicationName"];
            var version = _configuration["ApplicationSettings:Version"];
            var developerName = _configuration["ApplicationSettings:DeveloperName"];

            ViewBag.ApplicationName = _settings.ApplicationName;
            ViewBag.Version = _settings.Version;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
