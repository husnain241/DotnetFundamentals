using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Controllers
{
    [Route("students")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;

        public StudentController(
            IStudentService studentService,
            IDepartmentService departmentService)
        {
            _studentService = studentService;
            _departmentService = departmentService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var students = _studentService.GetAll();

            return View(students);
        }

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

        [HttpGet("create")]
        public IActionResult Create()
        {
            LoadDepartments();
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                LoadDepartments();
                return View(student);
            }

            _studentService.Add(student);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            LoadDepartments();
            return View(student);
        }

        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                LoadDepartments();
                return View(student);
            }

            _studentService.Update(student);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("transfer/{id:int}")]
        public IActionResult Transfer(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            LoadDepartments();
            return View(student);
        }

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
                LoadDepartments();
                return View(student);
            }

            _studentService.TransferDepartment(id, newDepartmentId);

            return RedirectToAction(nameof(Index));
        }

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

        private void LoadDepartments()
        {
            ViewBag.Departments = _departmentService.GetAll();
        }
    }
}