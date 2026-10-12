using Microsoft.AspNetCore.Mvc;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Services;

namespace UMS_Sp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        /// 1. Get all students (supports pagination, search, sorting)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<StudentDto>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _studentService.GetPagedAsync(pagination);
            return Ok(result);
        }

        /// <summary>
        /// 2. Get student by ID with department details
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentDetailDto>> GetById(int id)
        {
            var student = await _studentService.GetByIdWithDepartmentAsync(id);
            if (student == null)
            {
                return NotFound(new { message = $"Student with ID {id} not found." });
            }
            return Ok(student);
        }

        /// <summary>
        /// 3. Create new student
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _studentService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 4. Update student
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<StudentDto>> Update(int id, [FromBody] UpdateStudentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _studentService.UpdateAsync(id, dto);
                if (updated == null)
                {
                    return NotFound(new { message = $"Student with ID {id} not found." });
                }
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 5. Delete student
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _studentService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Student with ID {id} not found." });
            }
            return NoContent();
        }

        /// <summary>
        /// 6. Get students by department
        /// </summary>
        [HttpGet("by-department/{departmentId:int}")]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetByDepartment(int departmentId)
        {
            var students = await _studentService.GetStudentsByDepartmentAsync(departmentId);
            return Ok(students);
        }

        /// <summary>
        /// 7. Get student with all enrolled courses
        /// </summary>
        [HttpGet("{id:int}/courses")]
        public async Task<ActionResult<StudentWithCoursesDto>> GetStudentWithCourses(int id)
        {
            var result = await _studentService.GetStudentWithCoursesAsync(id);
            if (result == null)
            {
                return NotFound(new { message = $"Student with ID {id} not found." });
            }
            return Ok(result);
        }
    }
}
