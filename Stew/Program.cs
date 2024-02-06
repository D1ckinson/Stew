using System;
using System.Collections.Generic;
using System.Linq;

namespace Stew
{
    internal class Program
    {
        static void Main()
        {
            int stewQuantity = 10;
            int currentYear = 2024;
            List<Stew> stews = new List<Stew>();
            StewFabrik stewFabrik = new StewFabrik();

            for (int i = 0; i < stewQuantity; i++)
                stews.Add(stewFabrik.CreateStew());

            IEnumerable<Stew> expiredStew = stews.Where(stew => stew.ProductionYear + stew.ShelfLife <= currentYear);

            WriteStewsStats(stews, "Вся тушенка:");

            WriteStewsStats(expiredStew, "Просроченная тушенка:");
        }

        static void WriteStewsStats(IEnumerable<Stew> stews, string text)
        {
            Console.WriteLine(text);

            foreach (Stew stew in stews)
                Console.WriteLine($"Тушенка {stew.Name} произведена в {stew.ProductionYear} году." +
                    $" срок годности в годах: {stew.ShelfLife}.");

            Console.WriteLine();
        }
    }

    class Stew
    {
        public Stew(string name, int productionYear, int expirationDate)
        {
            Name = name;
            ProductionYear = productionYear;
            ShelfLife = expirationDate;
        }

        public string Name { get; private set; }
        public int ProductionYear { get; private set; }
        public int ShelfLife { get; private set; }
    }

    class StewFabrik
    {
        private List<string> _names;
        private int[] _productionYearsStats = { 2017, 2022 };
        private int[] _shelfLivesYearsStats = { 2, 10 };

        Random _random = new Random();

        public StewFabrik() =>
            FillNames();

        public Stew CreateStew()
        {
            string name = _names[_random.Next(_names.Count)];
            int productionYear = _random.Next(_productionYearsStats[0], _productionYearsStats[1]);
            int shelfLifeYears = _random.Next(_shelfLivesYearsStats[0], _shelfLivesYearsStats[1]);

            return new Stew(name, productionYear, shelfLifeYears);
        }

        private void FillNames() =>
            _names = new List<string>() { "Гродфуд", "Барс", "Совок", "Вкусвилл", "Микоян" };
    }
}
