using Microsoft.AspNetCore.Mvc;
using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // Read - Get all departments
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);
        }

        // Read - Get single department
        public IActionResult Details(int id)
        {
            var department = _departmentService.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
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
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            _departmentService.Add(department);

            return RedirectToAction(nameof(Index));
        }

        // Update - GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _departmentService.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // Update - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            var existingDepartment = _departmentService.GetById(department.Id);

            if (existingDepartment == null)
            {
                return NotFound();
            }

            _departmentService.Update(department);

            return RedirectToAction(nameof(Index));
        }

        // Delete - GET
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // Delete - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _departmentService.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            _departmentService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}