using UMS_Sp.DTOs;
using UMS_Sp.DTOs.Common;
using UMS_Sp.Models;
using UMS_Sp.Repositories;

namespace UMS_Sp.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EnrollmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EnrollmentDetailDto>> GetAllAsync()
        {
            return await _unitOfWork.Enrollments.GetAllDetailsAsync();
        }

        public async Task<PagedResult<EnrollmentDetailDto>> GetPagedAsync(PaginationParams pagination)
        {
            var (items, totalCount) = await _unitOfWork.Enrollments.GetPagedAsync(pagination);
            var dtos = items.Select(e => new EnrollmentDetailDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                StudentName = e.Student?.Name ?? string.Empty,
                StudentEmail = e.Student?.Email ?? string.Empty,
                CourseId = e.CourseId,
                CourseTitle = e.Course?.Title ?? string.Empty,
                CourseCredits = e.Course?.Credits ?? 0,
                EnrollmentDate = e.EnrollmentDate,
                Grade = e.Grade,
                Marks = e.Marks
            });

            return new PagedResult<EnrollmentDetailDto>(dtos, totalCount, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<EnrollmentDetailDto?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Enrollments.GetEnrollmentDetailsAsync(id);
        }

        public async Task<EnrollmentDetailDto> EnrollStudentAsync(CreateEnrollmentDto dto)
        {
            var studentExists = await _unitOfWork.Students.ExistsAsync(dto.StudentId);
            if (!studentExists)
            {
                throw new KeyNotFoundException($"Student with ID {dto.StudentId} does not exist.");
            }

            var courseExists = await _unitOfWork.Courses.ExistsAsync(dto.CourseId);
            if (!courseExists)
            {
                throw new KeyNotFoundException($"Course with ID {dto.CourseId} does not exist.");
            }

            var alreadyEnrolled = await _unitOfWork.Enrollments.IsEnrolledAsync(dto.StudentId, dto.CourseId);
            if (alreadyEnrolled)
            {
                throw new InvalidOperationException($"Student {dto.StudentId} is already enrolled in Course {dto.CourseId}.");
            }

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                EnrollmentDate = dto.EnrollmentDate ?? DateTime.UtcNow,
                Grade = dto.Grade?.ToUpper(),
                Marks = dto.Marks
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            await _unitOfWork.CompleteAsync();

            var createdDetail = await _unitOfWork.Enrollments.GetEnrollmentDetailsAsync(enrollment.Id);
            return createdDetail!;
        }

        public async Task<EnrollmentDetailDto?> UpdateEnrollmentAsync(int id, UpdateEnrollmentDto dto)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment == null) return null;

            enrollment.Grade = dto.Grade?.ToUpper();
            enrollment.Marks = dto.Marks;

            _unitOfWork.Enrollments.Update(enrollment);
            await _unitOfWork.CompleteAsync();

            return await _unitOfWork.Enrollments.GetEnrollmentDetailsAsync(id);
        }

        public async Task<bool> DeleteEnrollmentAsync(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);
            if (enrollment == null) return false;

            _unitOfWork.Enrollments.Delete(enrollment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await _unitOfWork.Enrollments.GetEnrollmentsByStudentIdAsync(studentId);
        }

        public async Task<IEnumerable<EnrollmentDetailDto>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await _unitOfWork.Enrollments.GetEnrollmentsByCourseIdAsync(courseId);
        }
    }
}
