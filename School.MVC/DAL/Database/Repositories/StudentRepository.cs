using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SchoolDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public StudentRepository(SchoolDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Create(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<Student> entities)
        {
            await _context.Students.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Student entity)
        {
            _context.Students.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Student> entities))
            {
                entities = await _context.Students.Include(s => s.Class).Take(rowsCount).ToListAsync();
                if (entities != null)
                {
                    _memoryCache.Set(cacheKey, entities,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return entities;
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
