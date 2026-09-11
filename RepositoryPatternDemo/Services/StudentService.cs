using RepositoryPatternDemo.Models;
using RepositoryPatternDemo.Repositories.Interfaces;
using RepositoryPatternDemo.Services.Interfaces;

namespace RepositoryPatternDemo.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Student> GetAll()
        {
            var students = _unitOfWork.Students.GetAll();
            foreach (var student in students)
            {
                PopulateDepartment(student);
            }
            return students;
        }

        public Student? GetById(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student != null)
            {
                PopulateDepartment(student);
            }
            return student;
        }

        public void Add(Student student)
        {
            _unitOfWork.Students.Add(student);

            // Increment StudentCount for the selected department
            if (student.DepartmentId.HasValue)
            {
                var dept = _unitOfWork.Departments.GetById(student.DepartmentId.Value);
                if (dept != null)
                {
                    dept.StudentCount++;
                    _unitOfWork.Departments.Update(dept);
                }
            }

            _unitOfWork.Complete();
        }

        public void Update(Student student)
        {
            _unitOfWork.Students.Update(student);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var student = _unitOfWork.Students.GetById(id);
            if (student != null)
            {
                if (student.DepartmentId.HasValue)
                {
                    var dept = _unitOfWork.Departments.GetById(student.DepartmentId.Value);
                    if (dept != null && dept.StudentCount > 0)
                    {
                        dept.StudentCount--;
                        _unitOfWork.Departments.Update(dept);
                    }
                }
                _unitOfWork.Students.Delete(id);
                _unitOfWork.Complete();
            }
        }

        public void TransferDepartment(int studentId, int newDepartmentId)
        {
            var student = _unitOfWork.Students.GetById(studentId);
            if (student == null)
            {
                throw new Exception("Student not found.");
            }

            if (student.DepartmentId == newDepartmentId)
            {
                return; // Already in target department
            }

            // 1. Decrease old department StudentCount
            if (student.DepartmentId.HasValue)
            {
                var oldDept = _unitOfWork.Departments.GetById(student.DepartmentId.Value);
                if (oldDept != null && oldDept.StudentCount > 0)
                {
                    oldDept.StudentCount--;
                    _unitOfWork.Departments.Update(oldDept);
                }
            }

            // 2. Increase new department StudentCount
            var newDept = _unitOfWork.Departments.GetById(newDepartmentId);
            if (newDept == null)
            {
                throw new Exception("Target department not found.");
            }
            newDept.StudentCount++;
            _unitOfWork.Departments.Update(newDept);

            // 3. Update student's DepartmentId
            student.DepartmentId = newDepartmentId;
            _unitOfWork.Students.Update(student);

            // 4. Save ALL changes together as ONE Unit of Work
            _unitOfWork.Complete();
        }

            var newDepartment = _unitOfWork.Departments.GetById(newDepartmentId);

            if (newDepartment == null)
            {
                throw new Exception("Target department not found.");
            }

            if (student.DepartmentId.HasValue)
            {
                UpdateDepartmentStudentCount(student.DepartmentId.Value, -1);
            }

            UpdateDepartmentStudentCount(newDepartmentId, 1);

            student.DepartmentId = newDepartmentId;
            _unitOfWork.Students.Update(student);

            _unitOfWork.Complete();
        }

        private void PopulateDepartment(Student student)
        {
            if (student.DepartmentId.HasValue)
            {
                student.Department = _unitOfWork.Departments.GetById(student.DepartmentId.Value);
            }
        }

        private void UpdateDepartmentStudentCount(int departmentId, int change)
        {
            var department = _unitOfWork.Departments.GetById(departmentId);

            if (department == null)
            {
                return;
            }

            department.StudentCount += change;
            _unitOfWork.Departments.Update(department);
        }


    }
}