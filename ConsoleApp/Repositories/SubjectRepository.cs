using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolDbContext _context;

        public SubjectRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task Create(Subject entity)
        {
            await _context.Subjects.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subject entity)
        {
            _context.Subjects.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subject>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Subjects.AsNoTracking().ToListAsync() :
                await _context.Subjects.ToListAsync();
        }

        public async Task<Subject> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Subjects.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Subject entity)
        {
            _context.Subjects.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}