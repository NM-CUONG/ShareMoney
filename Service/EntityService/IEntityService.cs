using Microsoft.EntityFrameworkCore;
namespace Service.EntityService
{
    public interface IEntityService<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void DeleteRange(IEnumerable<T> entities);
        void CreateRange(IEnumerable<T> entities);
        void UpdateRange(IEnumerable<T> entities);

    }
}
