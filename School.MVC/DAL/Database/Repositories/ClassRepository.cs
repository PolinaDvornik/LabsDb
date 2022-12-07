﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SchoolDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public ClassRepository(SchoolDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Create(Class entity)
        {
            await _context.Classes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(IEnumerable<Class> entities)
        {
            await _context.Classes.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Class entity)
        {
            _context.Classes.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Class>> Get(int rowsCount, string cacheKey)
        {
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<Class> entities))
            {
                entities = await _context.Classes.Include(c => c.ClassroomTeacher).Include(c => c.ClassType).Take(rowsCount).ToListAsync();
                if (entities != null)
                {
                    _memoryCache.Set(cacheKey, entities,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(256)));
                }
            }
            return entities;
        }

        public async Task<IEnumerable<Class>> GetAll(bool trackChanges)
        {
            return !trackChanges ? 
                await _context.Classes.AsNoTracking().ToListAsync() :
                await _context.Classes.ToListAsync();
        }

        public async Task<Class> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Classes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Classes.FirstOrDefaultAsync(e => e.Id == id); 
        }

        public async Task Update(Class entity)
        {
            _context.Classes.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
