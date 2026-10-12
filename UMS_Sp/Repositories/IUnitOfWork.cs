namespace UMS_Sp.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository Departments { get; }
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IEnrollmentRepository Enrollments { get; }
        Task<int> CompleteAsync();
    }
}
