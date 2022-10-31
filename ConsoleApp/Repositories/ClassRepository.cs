using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDbContext _context;

        public ClassRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task Create(Class entity)
        {
            await _context.Classes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Class entity)
        {
            _context.Classes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Class>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Classes.Include(c => c.ClassType).Include(c => c.ClassroomTeacher).AsNoTracking().ToListAsync() :
                await _context.Classes.Include(c => c.ClassType).Include(c => c.ClassroomTeacher).ToListAsync();
        }

        public async Task<Class> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Classes.Include(c => c.ClassType).Include(c => c.ClassroomTeacher).AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Classes.Include(c => c.ClassType).Include(c => c.ClassroomTeacher).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Class entity)
        {
            _context.Classes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}