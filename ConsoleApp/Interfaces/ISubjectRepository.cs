using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAll(bool trackChanges);
        Task<Subject> GetById(int id, bool trackChanges);
        Task Create(Subject entity);
        Task Delete(Subject entity);
        Task Update(Subject entity);
    }
}