using System;
using System.Collections.Generic;
using System.Linq;

namespace Stew
{
    class Program
    {
        static void Main()
        {
            int currentYear = 2024;
            StewFactory stewFactory = new StewFactory();

            List<Stew> stews = stewFactory.Create();
            List<Stew> expiredStew = stews.Where(stew => stew.ProductionYear + stew.ExpirationDate < currentYear).ToList();

            WriteStewsInfo(stews, "Вся тушенка:");
            WriteStewsInfo(expiredStew, "Просроченная тушенка:");
        }

        private static void WriteStewsInfo(List<Stew> stews, string text)
        {
            Console.WriteLine("\n" + text);
            stews.ForEach(stew => stew.WriteInfo());
        }
    }

    class Stew
    {
        public Stew(string name, int productionYear, int expirationDate)
        {
            Name = name;
            ProductionYear = productionYear;
            ExpirationDate = expirationDate;
        }

        public string Name { get; }
        public int ProductionYear { get; }
        public int ExpirationDate { get; }

        public void WriteInfo()
        {
            const int ShortOffset = -5;
            const int LongOffset = -10;

            Console.WriteLine($"{Name,LongOffset} Год производства: {ProductionYear,ShortOffset} " +
                $"Срок годности: {ExpirationDate,ShortOffset}");
        }
    }

    class StewFactory
    {
        public List<Stew> Create()
        {
            int[] productionYearsStats = { 2016, 2020 };
            int[] expirationDateStats = { 3, 8 };

            int stewsQuantity = 10;
            List<Stew> stews = new List<Stew>();

            for (int i = 0; i < stewsQuantity; i++)
            {
                string name = UserUtils.PickRandomValue(GetNames());
                int productionYear = UserUtils.GenerateStat(productionYearsStats);
                int expirationDate = UserUtils.GenerateStat(expirationDateStats);

                stews.Add(new Stew(name, productionYear, expirationDate));
            }

            return stews;
        }

        private List<string> GetNames() =>
            new List<string>() { "Гродфуд", "Барс", "Совок", "Вкусвилл", "Микоян" };
    }

    static class UserUtils
    {
        private static Random s_random = new Random();

        public static T PickRandomValue<T>(IEnumerable<T> values)
        {
            int index = s_random.Next(values.Count());

            return values.ElementAt(index);
        }

        public static int GenerateStat(int[] stats)
        {
            int maxLength = 2;

            if (stats.Length != maxLength)
            {
                throw new ArgumentException("Массив stats должен содержать ровно 2 элемента.");
            }

            return s_random.Next(stats[0], stats[1] + 1);
        }
    }
}
