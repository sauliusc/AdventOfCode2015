using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day9 : DayBase
    {
        struct TravelValue
        {
            public TravelValue(string city1, string city2, string distance)
            {
                City1 = city1;
                City2 = city2;
                Distance = Convert.ToInt32(distance);
            }
            
            public string City1 { get; set; }
            public string City2 { get; set; }
            public int Distance { get; set; }

            public bool EqualCities(string city1, string city2)
            {
                return (city1 == City1 && city2 == City2) || (city1 == City2 && city2 == City1);
            }
        }

        public override object GetFirstAnwer(string input)
        {
            return GetTravelResult(input, int.MaxValue, (totalDistance, result) => { return totalDistance < result; });
        }

        public override object GetSecondAnwer(string input)
        {
            return GetTravelResult(input, 0, (totalDistance, result) => { return totalDistance > result; });
        }

        public int GetTravelResult(string input, int startValue, Func<int, int, bool> compareAction)
        {
            int result = startValue;
            List<string> allCities = new List<string>();
            List<TravelValue> travelData = new List<TravelValue>();
            foreach (var line in input.SplitLines())
            {
                var items = line.Split(' ');
                if (!allCities.Contains(items[0]))
                    allCities.Add(items[0]);
                if (!allCities.Contains(items[2]))
                    allCities.Add(items[2]);
                travelData.Add(new TravelValue(items[0], items[2], items[4]));
            }
            List<List<string>> allPosibleValues = allCities.GeneratePermutations();
            foreach (var combination in allPosibleValues)
            {
                int totalDistance = 0;
                for (int i = 0; i < combination.Count() - 1; i++)
                {
                    totalDistance += travelData.First(item => item.EqualCities(combination[i], combination[i + 1])).Distance;
                }
                if (compareAction(totalDistance, result))
                {
                    result = totalDistance;
                }
            }

            return result;
        }

        public override string GetQuestion1()
        {
            return DataClass.Day9;
        }
    }
}
