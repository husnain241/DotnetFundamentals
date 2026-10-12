using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;
using UMS_Sp.Repositories;

namespace UMS_Sp.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseDto>> GetAllAsync()
        {
            var courses = await _unitOfWork.Courses.GetAllAsync();
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentId = c.DepartmentId
            });
        }

        public async Task<PagedResult<CourseDto>> GetPagedAsync(PaginationParams pagination)
        {
            var (items, totalCount) = await _unitOfWork.Courses.GetPagedAsync(pagination);
            var dtos = items.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentId = c.DepartmentId,
                DepartmentName = c.Department?.Name
            });

            return new PagedResult<CourseDto>(dtos, totalCount, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<CourseDto?> GetByIdAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdWithDepartmentAsync(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentId = course.DepartmentId,
                DepartmentName = course.Department?.Name
            };
        }

        public async Task<CourseDto> CreateAsync(CreateCourseDto dto)
        {
            var departmentExists = await _unitOfWork.Departments.ExistsAsync(dto.DepartmentId);
            if (!departmentExists)
            {
                throw new KeyNotFoundException($"Department with ID {dto.DepartmentId} does not exist.");
            }

            var course = new Course
            {
                Title = dto.Title,
                Credits = dto.Credits,
                DepartmentId = dto.DepartmentId
            };

            await _unitOfWork.Courses.AddAsync(course);
            await _unitOfWork.CompleteAsync();

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentId = course.DepartmentId
            };
        }

        public async Task<CourseDto?> UpdateAsync(int id, UpdateCourseDto dto)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null) return null;

            var departmentExists = await _unitOfWork.Departments.ExistsAsync(dto.DepartmentId);
            if (!departmentExists)
            {
                throw new KeyNotFoundException($"Department with ID {dto.DepartmentId} does not exist.");
            }

            course.Title = dto.Title;
            course.Credits = dto.Credits;
            course.DepartmentId = dto.DepartmentId;

            _unitOfWork.Courses.Update(course);
            await _unitOfWork.CompleteAsync();

            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Credits = course.Credits,
                DepartmentId = course.DepartmentId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course == null) return false;

            _unitOfWork.Courses.Delete(course);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByDepartmentAsync(int departmentId)
        {
            var courses = await _unitOfWork.Courses.GetCoursesByDepartmentAsync(departmentId);
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Credits = c.Credits,
                DepartmentId = c.DepartmentId,
                DepartmentName = c.Department?.Name
            });
        }

        public async Task<CourseWithStudentCountDto?> GetCourseWithStudentCountAsync(int id)
        {
            return await _unitOfWork.Courses.GetCourseWithStudentCountAsync(id);
        }

        public async Task<IEnumerable<CourseWithStudentCountDto>> GetAllWithStudentCountAsync()
        {
            return await _unitOfWork.Courses.GetAllWithStudentCountAsync();
        }
    }
}
