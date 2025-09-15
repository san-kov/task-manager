public class WorkTask : TaskItem
{
    string Project { get; }
    int EstimateHours { get; }
    public WorkTask(int id, string title, int priority, string project, int estimateHours) : base(id, title, priority)
    {
        if (estimateHours < 0) throw new ArgumentOutOfRangeException();

        Project = project;
        EstimateHours = estimateHours;
    }

    public override void PrintInfo()
    {

        base.PrintInfo();
        Console.Write($" Проект: {Project}, Часы: {EstimateHours}\n");
    }
}