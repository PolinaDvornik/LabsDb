using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetAll(bool trackChanges);
        Task<Teacher> GetById(int id, bool trackChanges);
        Task Create(Teacher entity);
        Task Delete(Teacher entity);
        Task Update(Teacher entity);
    }
}