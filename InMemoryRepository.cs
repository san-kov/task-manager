
public class InMemoryRepository<T> : IRepository<T> where T : IHasId
{
    private readonly List<T> _items = new();
    public void Add(T item)
    {
        _items.Add(item);
    }

    public T? FindById(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public IReadOnlyList<T> GetAll()
    {
        return _items;
    }

    public bool Remove(int id)
    {
        return _items.RemoveAll(x => x.Id == id) > 0;
    }

    public IEnumerable<T> Where(Func<T, bool> predicate) =>
        _items.Where(predicate);
}