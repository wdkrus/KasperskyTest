using System;
using System.Collections.Generic;
using System.Linq;

namespace Test2
{
    class Program
    {
        const int collectionLength = 1000;
        const int maxValue = 100;

        static void Main(string[] args)
        {
            Random rnd = new Random();

            List<int> items = Enumerable.Range(0, collectionLength)
                .Select(x => rnd.Next(maxValue + 1)).ToList();

            Console.WriteLine("Collection is: " + items.Select(x => x.ToString())
                .Aggregate((f, s) => f + "," + s));

            //Заведомо не может быть больше суммы двух maxValue
            int testValue = rnd.Next((maxValue * 2) + 1);

            Console.WriteLine();
            Console.WriteLine("Item is: {0}", testValue);
            Console.WriteLine();

            List<NumberPair> pairs = GetPairs(items, testValue).Distinct().ToList();

            Console.WriteLine("Pairs are: ");
            foreach (var pair in pairs)
                Console.WriteLine(pair);

            Console.ReadKey();
        }

        static List<NumberPair> GetPairs(List<int> items, int testValue)
        {
            List<NumberPair> result = new List<NumberPair>();

            HashSet<int> hash = new HashSet<int>();

            for (int i = 0; i < items.Count; i++)
            {
                int delta = testValue - items[i];

                if (hash.Contains(delta))
                    result.Add(new NumberPair() { A = items[i], B = delta });
                else
                    hash.Add(items[i]);
            }

            return result;
        }
    }
}