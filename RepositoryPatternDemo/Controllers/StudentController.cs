using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // Read - Get all students
        public IActionResult Index()
        {
            var students = _studentService.GetAll();

            return View(students);
        }

        // Read - Get single student
        public IActionResult Details(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            _studentService.Add(student);

            return RedirectToAction(nameof(Index));
        }

        // Update - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // Update - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            var existingStudent = _studentService.GetById(student.Id);

            if (existingStudent == null)
            {
                return NotFound();
            }

            _studentService.Update(student);

            return RedirectToAction(nameof(Index));
        }

        // Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            _studentService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}