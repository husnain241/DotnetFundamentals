using Microsoft.AspNetCore.Mvc;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Services;

namespace UMS_Sp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// 1. Get all courses (supports pagination, search, sorting)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<CourseDto>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _courseService.GetPagedAsync(pagination);
            return Ok(result);
        }

        /// <summary>
        /// 2. Get course by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CourseDto>> GetById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound(new { message = $"Course with ID {id} not found." });
            }
            return Ok(course);
        }

        /// <summary>
        /// 3. Create new course
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CourseDto>> Create([FromBody] CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _courseService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 4. Update course
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CourseDto>> Update(int id, [FromBody] UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _courseService.UpdateAsync(id, dto);
                if (updated == null)
                {
                    return NotFound(new { message = $"Course with ID {id} not found." });
                }
                return Ok(updated);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// 5. Delete course
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _courseService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Course with ID {id} not found." });
            }
            return NoContent();
        }

        /// <summary>
        /// 6. Get courses by department
        /// </summary>
        [HttpGet("by-department/{departmentId:int}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetByDepartment(int departmentId)
        {
            var courses = await _courseService.GetCoursesByDepartmentAsync(departmentId);
            return Ok(courses);
        }

        /// <summary>
        /// 7. Get course with enrolled students count
        /// </summary>
        [HttpGet("{id:int}/students-count")]
        public async Task<ActionResult<CourseWithStudentCountDto>> GetCourseWithStudentCount(int id)
        {
            var result = await _courseService.GetCourseWithStudentCountAsync(id);
            if (result == null)
            {
                return NotFound(new { message = $"Course with ID {id} not found." });
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all courses with enrolled students count
        /// </summary>
        [HttpGet("with-students-count")]
        public async Task<ActionResult<IEnumerable<CourseWithStudentCountDto>>> GetAllWithStudentCount()
        {
            var results = await _courseService.GetAllWithStudentCountAsync();
            return Ok(results);
        }
    }
}
