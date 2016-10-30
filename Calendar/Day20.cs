using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day20 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            int value = Convert.ToInt32(input);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ConcurrentDictionary<int, int> houses = new ConcurrentDictionary<int, int>();
            ParallelOptions opt = new ParallelOptions { MaxDegreeOfParallelism = 1000 };
            Parallel.ForEach(Enumerable.Range(1, value), opt, (item) => { houses.TryAdd(item, 10); });
            sw.Stop();
            Console.WriteLine("Elapsed to create array:{0}", sw.ElapsedMilliseconds);
            sw.Restart();
            sw.Start();
            Parallel.For(2, value, opt, (i) =>
            {
                var list = Enumerable.Range(1, (value / i)).Select(x => x * i);
                foreach (var item in list)
                {
                    houses[item] += i * 10;
                };
            });
            var results = houses.Where(litem => litem.Value >= value);
            sw.Stop();
            Console.WriteLine("Elapsed to find:{0}", sw.ElapsedMilliseconds);
            return results.First().Key;
        }

        public override string GetQuestion1()
        {
            return "29000000";
        }

        public override object GetSecondAnwer(string input)
        {
            int value = Convert.ToInt32(input);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int maxHouses = 50;
            ConcurrentDictionary<int, int> houses = new ConcurrentDictionary<int, int>();
            ParallelOptions opt = new ParallelOptions { MaxDegreeOfParallelism = 1000 };
            Parallel.ForEach(Enumerable.Range(1, value), opt, (item) => { houses.TryAdd(item, 11); });
            sw.Stop();
            Console.WriteLine("Elapsed to create array:{0}", sw.ElapsedMilliseconds);
            sw.Restart();
            sw.Start();
            Parallel.For(2, value, opt, (i) =>
            {
                var list = Enumerable.Range(1, maxHouses).Select(x => x * i).Where(xx => xx <= value);
                foreach (var item in list)
                {
                    houses[item] += i * 11;
                };
            });
            var results = houses.Where(litem => litem.Value >= value);
            sw.Stop();
            Console.WriteLine("Elapsed to find:{0}", sw.ElapsedMilliseconds);
            return results.First().Key;
        }
    }
}
