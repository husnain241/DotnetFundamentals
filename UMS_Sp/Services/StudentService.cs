using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;
using UMS_Sp.Repositories;

namespace UMS_Sp.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                EnrollmentDate = s.EnrollmentDate,
                DepartmentId = s.DepartmentId
            });
        }

        public async Task<PagedResult<StudentDto>> GetPagedAsync(PaginationParams pagination)
        {
            var (items, totalCount) = await _unitOfWork.Students.GetPagedAsync(pagination);
            var dtos = items.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                EnrollmentDate = s.EnrollmentDate,
                DepartmentId = s.DepartmentId
            });

            return new PagedResult<StudentDto>(dtos, totalCount, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<StudentDetailDto?> GetByIdWithDepartmentAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdWithDepartmentAsync(id);
            if (student == null) return null;

            return new StudentDetailDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                EnrollmentDate = student.EnrollmentDate,
                DepartmentId = student.DepartmentId,
                DepartmentName = student.Department?.Name ?? string.Empty,
                HeadOfDepartment = student.Department?.HeadOfDepartment ?? string.Empty
            };
        }

        public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
        {
            var departmentExists = await _unitOfWork.Departments.ExistsAsync(dto.DepartmentId);
            if (!departmentExists)
            {
                throw new KeyNotFoundException($"Department with ID {dto.DepartmentId} does not exist.");
            }

            var emailTaken = await _unitOfWork.Students.EmailExistsAsync(dto.Email);
            if (emailTaken)
            {
                throw new InvalidOperationException($"Student with email '{dto.Email}' already exists.");
            }

            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                EnrollmentDate = dto.EnrollmentDate ?? DateTime.UtcNow,
                DepartmentId = dto.DepartmentId
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.CompleteAsync();

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                EnrollmentDate = student.EnrollmentDate,
                DepartmentId = student.DepartmentId
            };
        }

        public async Task<StudentDto?> UpdateAsync(int id, UpdateStudentDto dto)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return null;

            var departmentExists = await _unitOfWork.Departments.ExistsAsync(dto.DepartmentId);
            if (!departmentExists)
            {
                throw new KeyNotFoundException($"Department with ID {dto.DepartmentId} does not exist.");
            }

            var emailTaken = await _unitOfWork.Students.EmailExistsAsync(dto.Email, id);
            if (emailTaken)
            {
                throw new InvalidOperationException($"Student with email '{dto.Email}' already exists.");
            }

            student.Name = dto.Name;
            student.Email = dto.Email;
            student.DepartmentId = dto.DepartmentId;

            _unitOfWork.Students.Update(student);
            await _unitOfWork.CompleteAsync();

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                EnrollmentDate = student.EnrollmentDate,
                DepartmentId = student.DepartmentId
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student == null) return false;

            _unitOfWork.Students.Delete(student);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByDepartmentAsync(int departmentId)
        {
            var students = await _unitOfWork.Students.GetStudentsByDepartmentAsync(departmentId);
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                EnrollmentDate = s.EnrollmentDate,
                DepartmentId = s.DepartmentId
            });
        }

        public async Task<StudentWithCoursesDto?> GetStudentWithCoursesAsync(int id)
        {
            var student = await _unitOfWork.Students.GetStudentWithCoursesAsync(id);
            if (student == null) return null;

            return new StudentWithCoursesDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                EnrollmentDate = student.EnrollmentDate,
                DepartmentId = student.DepartmentId,
                DepartmentName = student.Department?.Name ?? string.Empty,
                EnrolledCourses = student.Enrollments.Select(e => new EnrolledCourseDto
                {
                    EnrollmentId = e.Id,
                    CourseId = e.CourseId,
                    CourseTitle = e.Course?.Title ?? string.Empty,
                    Credits = e.Course?.Credits ?? 0,
                    EnrollmentDate = e.EnrollmentDate,
                    Grade = e.Grade,
                    Marks = e.Marks
                }).ToList()
            };
        }

        public async Task<StudentDto> GetByIdUsingStoredProcedureAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdUsingStoredProcedureAsync(id);

            if (student == null)
                throw new KeyNotFoundException($"Student with ID {id} not found");

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                EnrollmentDate = student.EnrollmentDate,
                DepartmentId = student.DepartmentId
            };
        }
    }
}
