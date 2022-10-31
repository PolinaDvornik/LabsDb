using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class ClassTypeRepository : IClassTypeRepository
    {
        private readonly SchoolDbContext _context;

        public ClassTypeRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task Create(ClassType entity)
        {
            await _context.ClassTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ClassType entity)
        {
            _context.ClassTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClassType>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.ClassTypes.AsNoTracking().ToListAsync() :
                await _context.ClassTypes.ToListAsync();
        }

        public async Task<ClassType> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.ClassTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.ClassTypes.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(ClassType entity)
        {
            _context.ClassTypes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}