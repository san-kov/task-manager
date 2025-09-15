public class TaskItem : IPrintable
{
    public int Id { get; init; }
    public string Title { get; init; } = "";
    public int Priority { get; init; }
    public TaskDate? DueDate { get; init; }

    public TaskItem(int id, string title, int priority)
    {
        Id = id;
        Title = title;
        Priority = priority;
    }

    static string ConvertPriority(int p)
    {
        return p switch
        {
            1 => "Низкий",
            2 => "Средний",
            3 => "Высокий",
            _ => "Неизвестный"
        };
    }

    public virtual void PrintInfo()
    {
        var due = DueDate is null ? "—" : DueDate.ToString();
        Console.WriteLine($"{Id}. {Title,-20} приоритет: {ConvertPriority(Priority)}, дедлайн: {due}");
    }
}