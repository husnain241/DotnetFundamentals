using AspNetCoreFundamentals.Filters;
using AspNetCoreFundamentals.Interface;
using AspNetCoreFundamentals.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreFundamentals.Controllers
{
   

    [Route("students")]
    [ServiceFilter(typeof(LoggingFilter))]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            IStudentService studentService,
            ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = _studentService.GetAll();

            Console.WriteLine($"Students: {students.Count()}");

            return View("Index", students);
        }

        // GET /students/5
        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET /students/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST /students/create
        [HttpPost("create")]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            try
            {
                _studentService.Add(student);

                TempData["Success"] =
                    "Student created successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);

                return View(student);
            }
        }

        // GET /students/edit/5
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST /students/edit/5
        [HttpPost("edit/{id:int}")]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(student);
            }

            try
            {
                _studentService.Update(student);

                TempData["Success"] =
                    "Student updated successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // POST /students/delete/5
        [HttpPost("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);

                TempData["Success"] =
                    "Student deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
