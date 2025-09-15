Console.InputEncoding = System.Text.Encoding.GetEncoding("utf-16");
List<TaskItem> tasks = new();

for (int i = 0; i < 5; i++)
{
    Console.WriteLine("Введите название задачи:");

    string? task = Console.ReadLine();

    if (string.IsNullOrEmpty(task))
    {
        Console.WriteLine("Недопустимое название");
        i--;
        continue;
    }

    Console.WriteLine("Введите приоритет (1-3):");

    string? priorityText = Console.ReadLine();
    int priority = 1;

    if (!string.IsNullOrEmpty(priorityText))
    {
        try
        {
            priority = int.Parse(priorityText);
            if (priority < 1 || priority > 3) throw new ArgumentOutOfRangeException();
        }
        catch (Exception)
        {
            Console.WriteLine("Неправильный диапазон");
            i--;
            continue;
        }
    }

    Console.WriteLine("Дедлайн: да/нет");
    string? answer = Console.ReadLine();
    TaskDate? date = null;

    if (!string.IsNullOrEmpty(answer) && answer.Equals("да", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Введите дату YYYY-MM-DD");
        var dateString = Console.ReadLine();

        if (DateOnly.TryParseExact(dateString, "yyyy-MM-dd", out var d))
        {
            date = new TaskDate(d.Year, d.Month, d.Day);
        }
        else
        {
            Console.WriteLine("Неверная дата");
        }

    }

    TaskItem taskItem = new TaskItem(i + 1, task, priority) { DueDate = date };
    tasks.Add(taskItem);

}

foreach (TaskItem t in tasks)
{
    t.PrintInfo();
}

Console.WriteLine("Введите приоритет для фильтрации");
string? filter = Console.ReadLine();

try
{
    int filterInt = int.Parse(filter!);

    foreach (TaskItem i in tasks)
    {
        if (i.Priority == filterInt)
        {
            i.PrintInfo();
        }
    }

}
catch
{
    Console.WriteLine("Неверный ввод");
}


public class TaskItem
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

    public void PrintInfo()
    {
        var due = DueDate is null ? "—" : DueDate.ToString();
        Console.WriteLine($"{Id}. {Title,-20} приоритет: {ConvertPriority(Priority)}, дедлайн: {due}");
    }
}

public readonly struct TaskDate
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }

    public TaskDate(int year, int month, int day)
    {
        if (year < 1) throw new ArgumentOutOfRangeException();
        if (month < 1 || month > 12) throw new ArgumentOutOfRangeException();
        if (day < 1 || day > 31) throw new ArgumentOutOfRangeException();

        Year = year;
        Month = month;
        Day = day;
    }

    public override string ToString() => $"{Year:D4}-{Month:D2}-{Day:D2}";

}