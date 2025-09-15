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