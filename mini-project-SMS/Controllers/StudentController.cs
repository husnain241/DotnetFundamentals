using Microsoft.AspNetCore.Mvc;
using mini_project_SMS.Models;
using mini_project_SMS.Services;
using mini_project_SMS.Services.Interfaces;

namespace mini_project_SMS.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IDepartmentService _departmentService;
        private readonly IImageService _imageService;

        public StudentController(
    IStudentService studentService,
    IImageService imageService,
    IDepartmentService departmentService)
        {
            _studentService = studentService;
            _imageService = imageService;
            _departmentService = departmentService;
        }

public IActionResult Index(string? search)
{
    var students = _studentService.Search(search ?? "");

    var departments = _departmentService.GetAll();

    var departmentNames = departments.ToDictionary(
        d => d.Id,
        d => d.Name);

    ViewBag.DepartmentNames = departmentNames;
    ViewBag.Search = search;

    return View(students);
}
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
     Student student,
     IFormFile? image)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            try
            {
                if (image != null)
                {
                    student.ImagePath =
                        await _imageService.SaveImageAsync(image);
                }

                _studentService.Add(student);

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Image", ex.Message);

                return View(student);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
     Student student,
     IFormFile? image)
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

            try
            {
                if (image != null)
                {
                    var oldImagePath = existingStudent.ImagePath;

                    student.ImagePath =
                        await _imageService.SaveImageAsync(image);

                    _imageService.DeleteImage(oldImagePath);
                }
                else
                {
                    student.ImagePath = existingStudent.ImagePath;
                }

                var updated = _studentService.Update(student);

                if (!updated)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("image", ex.Message);

                ViewBag.Departments = _departmentService.GetAll();


                return View(student);
            }
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            _imageService.DeleteImage(student.ImagePath);

            var deleted = _studentService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
