using Microsoft.EntityFrameworkCore.Storage;
using RepositoryPatternDemo.Data;
using RepositoryPatternDemo.Repositories;
using RepositoryPatternDemo.Repositories.Interfaces;

namespace RepositoryPatternDemo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public IStudentRepository Students { get; }
        public IDepartmentRepository Departments { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Students = new StudentRepository(_context);
            Departments = new DepartmentRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction.Dispose();
                _transaction = null;
            }
        }
        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _context.Dispose();
        }
    }
}