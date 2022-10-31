﻿using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAll(bool trackChanges);
        Task<Schedule> GetById(int id, bool trackChanges);
        Task Create(Schedule entity);
        Task Delete(Schedule entity);
        Task Update(Schedule entity);
    }
}