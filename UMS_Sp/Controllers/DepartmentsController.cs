using Microsoft.AspNetCore.Mvc;
using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Services;

namespace UMS_Sp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// 1. Get all departments (supports pagination, search, sorting)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<PagedResult<DepartmentDto>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _departmentService.GetPagedAsync(pagination);
            return Ok(result);
        }

        /// <summary>
        /// 2. Get department by ID
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DepartmentDto>> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }
            return Ok(department);
        }

        /// <summary>
        /// 3. Create new department
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateDepartmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _departmentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        /// <summary>
        /// 4. Update department
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<ActionResult<DepartmentDto>> Update(int id, [FromBody] UpdateDepartmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _departmentService.UpdateAsync(id, dto);
            if (updated == null)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }
            return Ok(updated);
        }

        /// <summary>
        /// 5. Delete department
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _departmentService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }
            return NoContent();
        }

        /// <summary>
        /// 6. Get department with total students count
        /// </summary>
        [HttpGet("{id:int}/students-count")]
        public async Task<ActionResult<DepartmentWithStudentCountDto>> GetDepartmentWithStudentCount(int id)
        {
            var result = await _departmentService.GetDepartmentWithStudentCountAsync(id);
            if (result == null)
            {
                return NotFound(new { message = $"Department with ID {id} not found." });
            }
            return Ok(result);
        }

        /// <summary>
        /// Get all departments with total students count
        /// </summary>
        [HttpGet("with-students-count")]
        public async Task<ActionResult<IEnumerable<DepartmentWithStudentCountDto>>> GetAllWithStudentCount()
        {
            var results = await _departmentService.GetAllWithStudentCountAsync();
            return Ok(results);
        }
    }
}
