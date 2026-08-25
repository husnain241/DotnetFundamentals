using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;

        public StudentController(IStudentService studentService, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
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
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        // Create - POST
        [HttpPost]
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
        [HttpGet]
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
        [HttpPost]
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
        [HttpGet]
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
        [HttpPost]
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