using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using School.MVC.DAL.Interfaces.Repositories;
using School.MVC.DAL.Models;

namespace School.MVC.DAL.Database.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly SchoolDbContext _context;

        public ScheduleRepository(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task Create(Schedule entity)
        {
            await _context.Schedules.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Schedule entity)
        {
            _context.Schedules.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Schedule>> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                await _context.Schedules.AsNoTracking().ToListAsync() :
                await _context.Schedules.ToListAsync();
        }

        public async Task<Schedule> GetById(int id, bool trackChanges)
        {
            return !trackChanges ?
                await _context.Schedules.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id) :
                await _context.Schedules.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Update(Schedule entity)
        {
            _context.Schedules.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}