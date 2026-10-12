using Microsoft.AspNetCore.Mvc;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Services;

namespace UMS_Sp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentsController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        /// <summary>
        /// Get all enrollments (supports pagination, search, sorting)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<EnrollmentDetailDto>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _enrollmentService.GetPagedAsync(pagination);
            return Ok(result);
        }

        /// <summary>
        /// 6. Get enrollment details (student name, course title, grade, marks) by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EnrollmentDetailDto>> GetById(int id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound(new { message = $"Enrollment with ID {id} not found." });
            }
            return Ok(enrollment);
        }

        /// <summary>
        /// 1. Enroll student in course
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EnrollmentDetailDto>> EnrollStudent([FromBody] CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _enrollmentService.EnrollStudentAsync(dto);
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
        /// 2. Update enrollment (marks, grade)
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<EnrollmentDetailDto>> UpdateEnrollment(int id, [FromBody] UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _enrollmentService.UpdateEnrollmentAsync(id, dto);
            if (updated == null)
            {
                return NotFound(new { message = $"Enrollment with ID {id} not found." });
            }
            return Ok(updated);
        }

        /// <summary>
        /// 3. Delete enrollment
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            var deleted = await _enrollmentService.DeleteEnrollmentAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Enrollment with ID {id} not found." });
            }
            return NoContent();
        }

        /// <summary>
        /// 4. Get all enrollments for a student
        /// </summary>
        [HttpGet("student/{studentId:int}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDetailDto>>> GetByStudent(int studentId)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByStudentIdAsync(studentId);
            return Ok(enrollments);
        }

        /// <summary>
        /// 5. Get all enrollments for a course
        /// </summary>
        [HttpGet("course/{courseId:int}")]
        public async Task<ActionResult<IEnumerable<EnrollmentDetailDto>>> GetByCourse(int courseId)
        {
            var enrollments = await _enrollmentService.GetEnrollmentsByCourseIdAsync(courseId);
            return Ok(enrollments);
        }
    }
}
