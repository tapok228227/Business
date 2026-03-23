using System.Text.Json;
using System.IO;

namespace Business
{
    public class SelfEmployedTracker
    {
        private List<Income> incomes = new List<Income>();
        private int nextId = 1;
        private const string FILE = "incomes.json";

        public void Add(DateTime date, decimal amount, string customer, bool isIndividual)
        {
            incomes.Add(new Income
            {
                Id = nextId++,
                Date = date,
                Amount = amount,
                Customer = customer,
                IsIndividualCustomer = isIndividual
            });
            SaveToJson();
        }

        public decimal GetMonthlyIncome(int year, int month)
        {
            decimal sum = 0;
            foreach (var i in incomes)
                if (i.Date.Year == year && i.Date.Month == month)
                    sum += i.Amount;
            return sum;
        }

        public void PrintMonthlyAnalytics(int year, int month)
        {
            decimal total = 0;
            decimal individual = 0;

            foreach (var i in incomes)
                if (i.Date.Year == year && i.Date.Month == month)
                {
                    total += i.Amount;
                    if (i.IsIndividualCustomer) individual += i.Amount;
                }

            decimal legal = total - individual;
            decimal taxIndividual = individual * 0.04m;
            decimal taxLegal = legal * 0.06m;
            decimal totalTax = taxIndividual + taxLegal;
            decimal profit = total - totalTax;

            Console.WriteLine($"Доход: {total}р");
            Console.WriteLine($"Налог физлиц: {taxIndividual}р");
            Console.WriteLine($"Налог юрлиц: {taxLegal}р");
            Console.WriteLine($"Общий налог: {totalTax}р");
            Console.WriteLine($"Прибыль: {profit}р");
        }

        private void SaveToJson()
        {
            string json = JsonSerializer.Serialize(incomes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FILE, json);
        }

        public void LoadFromJson()
        {
            if (File.Exists(FILE))
            {
                string json = File.ReadAllText(FILE);
                incomes = JsonSerializer.Deserialize<List<Income>>(json) ?? new List<Income>();
                nextId = incomes.Count > 0 ? incomes.Max(i => i.Id) + 1 : 1;
            }
        }
    }
}
