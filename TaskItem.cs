public class TaskItem : IPrintable, IHasId
{
    public int Id { get; init; }
    public string Title { get; init; } = "";
    public Priority Priority { get; init; }
    public TaskDate? DueDate { get; init; }

    public TaskItem(int id, string title, Priority priority)
    {
        Id = id;
        Title = title;
        Priority = priority;
    }

    static string ConvertPriority(Priority p)
    {
        return p switch
        {
            Priority.Low => "Низкий",
            Priority.Medium => "Средний",
            Priority.High => "Высокий",
            _ => "Неизвестный"
        };
    }

    public virtual void PrintInfo()
    {
        var due = DueDate is null ? "—" : DueDate.ToString();
        Console.WriteLine($"{Id}. {Title,-20} приоритет: {ConvertPriority(Priority)}, дедлайн: {due}");
    }
}