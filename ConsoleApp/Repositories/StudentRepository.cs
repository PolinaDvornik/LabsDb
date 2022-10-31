using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;

        public StudentRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task Create(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student entity)
        {
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Students.AsNoTracking().ToListAsync() :
                await _context.Students.ToListAsync();
        }

        public async Task<Student> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Students.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Students.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}