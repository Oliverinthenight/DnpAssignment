namespace RepositoryContracts;

public interface IRepository<T>
{
    void Create(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
    void Update(T entity);
    void Delete(int id);
}
 