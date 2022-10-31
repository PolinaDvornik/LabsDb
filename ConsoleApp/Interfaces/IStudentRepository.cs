using School.MVC.DAL.Models;

namespace School.MVC.DAL.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll(bool trackChanges);
        Task<Student> GetById(int id, bool trackChanges);
        Task Create(Student entity);
        Task Delete(Student entity);
        Task Update(Student entity);
    }
}