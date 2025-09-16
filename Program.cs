Console.InputEncoding = System.Text.Encoding.GetEncoding("utf-16");
IRepository<TaskItem> repo = new InMemoryRepository<TaskItem>();

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


    if (!int.TryParse(priorityText, out priority) || priority < 1 || priority > 3)
    {
        Console.WriteLine("Неправильный диапазон");
        i--;
        continue;
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

    Console.WriteLine("Рабочая задача: да/нет");
    string? isWorkingTask = Console.ReadLine();
    if (!string.IsNullOrEmpty(isWorkingTask) && isWorkingTask.Equals("да", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Проект: ");
        string? project = Console.ReadLine();
        Console.WriteLine("Часы: ");
        string? hours = Console.ReadLine();
        int id = repo.GetAll().Count + 1;

        if (int.TryParse(hours, out var intHours) && !string.IsNullOrEmpty(project))
        {
            WorkTask taskItem = new WorkTask(id, task, (Priority)priority, project, intHours) { DueDate = date };
            repo.Add(taskItem);
        }
        else
        {
            continue;
        }
    }
    else
    {
        TaskItem taskItem = new TaskItem(i + 1, task, (Priority)priority) { DueDate = date };
        repo.Add(taskItem);
    }

}

IEnumerable<IPrintable> printable = repo.GetAll();

foreach (var t in printable)
{
    t.PrintInfo();
}

Console.WriteLine("Введите приоритет для фильтрации");
string? filter = Console.ReadLine();

TaskFilter isHigh = t => t.Priority == Priority.High;
TaskFilter hasDeadline = t => t.DueDate is not null;


TaskFilter highWithDeadline = t => isHigh(t) && hasDeadline(t);
foreach (var t in repo.Where(x => highWithDeadline(x))) t.PrintInfo();

try
{
    int filterInt = int.Parse(filter!);
    TaskFilter byPriority = t => t.Priority == (Priority)filterInt;

    foreach (var i in repo.Where(task => byPriority(task)))
    {
        i.PrintInfo();

    }

}
catch
{
    Console.WriteLine("Неверный ввод");
}

