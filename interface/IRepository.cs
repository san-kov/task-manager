public interface IRepository<T> where T : IHasId
{
    void Add(T item);
    bool Remove(int id);
    T? FindById(int id);
    IReadOnlyList<T> GetAll();
}