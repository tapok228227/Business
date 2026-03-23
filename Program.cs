using Business;

class Program
{
    static void Main()
    {
        var tracker = new SelfEmployed тмTracker();
        tracker.LoadFromJson();

        tracker.Add(new DateTime(2026, 3, 5), 5000, "Иванов", true);
        tracker.Add(new DateTime(2026, 3, 12), 15000, "ООО Ромашка", false);

        Console.WriteLine("Аналитика за март 2026:");
        tracker.PrintMonthlyAnalytics(2026, 3);

        Console.WriteLine($"\nДоход за месяц: {tracker.GetMonthlyIncome(2026, 3)}р");
    }
}