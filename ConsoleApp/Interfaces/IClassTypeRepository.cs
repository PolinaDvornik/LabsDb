using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface IClassTypeRepository
    {
        Task<IEnumerable<ClassType>> GetAll(bool trackChanges);
        Task<ClassType> GetById(int id, bool trackChanges);
        Task Create(ClassType entity);
        Task Delete(ClassType entity);
        Task Update(ClassType entity);
    }
}