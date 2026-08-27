using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Controllers
{
    [Route("students")]
    public class StudentController : Controller
    {
        // Dependency injection for services

        // DI for IStudentService and IDepartmentService
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;

        public StudentController(IStudentService studentService, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
        }

        // Read - Get all students
        [HttpGet("")]
        public IActionResult Index()
        {
            var students = _studentService.GetAll();

            return View(students);
        }

        // Read - Get single student
        [HttpGet("details/{id:int}")]
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
        [HttpGet("create")]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        // Create - POST
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentService.GetAll();
                return View(student);
            }

            _studentService.Add(student);

            return RedirectToAction(nameof(Index));
        }

        // Update - GET
        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _departmentService.GetAll();
            return View(student);
        }

        // Update - POST
        [HttpPost("edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentService.GetAll();
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

        // Transfer Department - GET
        [HttpGet("transfer/{id:int}")]
        public IActionResult Transfer(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _departmentService.GetAll();
            return View(student);
        }

        // Transfer Department - POST
        [HttpPost("transfer/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer(int id, int newDepartmentId)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            if (newDepartmentId <= 0)
            {
                ModelState.AddModelError("", "Please select a valid department.");
                ViewBag.Departments = _departmentService.GetAll();
                return View(student);
            }

            _studentService.TransferDepartment(id, newDepartmentId);

            return RedirectToAction(nameof(Index));
        }

        // Delete - GET
        [HttpGet("delete/{id:int}")]
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
        [HttpPost("delete/{id:int}")]

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