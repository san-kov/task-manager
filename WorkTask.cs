public class WorkTask : TaskItem, IHasId
{
    string Project { get; }
    int EstimateHours { get; }
    public WorkTask(int id, string title, Priority priority, string project, int estimateHours) : base(id, title, priority)
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