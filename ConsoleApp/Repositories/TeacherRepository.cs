using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SchoolDbContext _context;

        public TeacherRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task Create(Teacher entity)
        {
            await _context.Teachers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Teacher entity)
        {
            _context.Teachers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Teachers.AsNoTracking().ToListAsync() :
                await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Teachers.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Teachers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Teacher entity)
        {
            _context.Teachers.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}