using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar
{
    public class Day17 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            List<int> allContainers = new List<int>();
            foreach (var item in input.SplitLines())
            {
                allContainers.Add(Convert.ToInt32(item));
            };
            return allContainers.GetCombinationsBySum(150, new List<int>()).Count();

        }

        public override string GetQuestion1()
        {
            return DataClass.Day17;
        }

        public override object GetSecondAnwer(string input)
        {
            List<int> allContainers = new List<int>();
            foreach (var item in input.SplitLines())
            {
                allContainers.Add(Convert.ToInt32(item));
            };
            var combinations = allContainers.GetCombinationsBySum(150, new List<int>());
            var minQty = combinations.Min(item => item.Count());
            return combinations.Count(item => item.Count() == minQty);
        }
    }
}
