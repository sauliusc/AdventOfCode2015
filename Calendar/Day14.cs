using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day14 : DayBase
    {
        class Reindeer
        {
            public Reindeer(string reindeerData)
            {
                var data = reindeerData.Split(' ');
                Name = data[0];
                FlySpeed = Convert.ToInt32(data[3]);
                FlyTime = Convert.ToInt32(data[6]);
                RestTime = Convert.ToInt32(data[13]);

            }

            public String Name { get; set; }
            public int FlySpeed { get; set; }
            public int FlyTime { get; set; }
            public int RestTime { get; set; }
            public int TotalPoints { get; set; }
            public int TotalTraveleDistance { get; set; }
            public void RecalculateTraveledDistance(int travelDuration)
            {
                int traveledTime = 0;
                int traveledDistance = 0;
                bool restTime = false;
                while (traveledTime < travelDuration)
                {
                    if (restTime)
                    {
                        traveledTime += RestTime;
                        restTime = false;
                    }
                    else
                    {
                        if ((traveledTime + FlyTime) <= travelDuration)
                        {
                            traveledTime += FlyTime;
                            traveledDistance += FlySpeed * FlyTime;
                        }
                        else
                        {
                            traveledDistance += FlySpeed * (travelDuration - traveledTime);
                            traveledTime = travelDuration;
                        }
                        restTime = true;
                    }
                }
                TotalTraveleDistance = traveledDistance;

            }

            public void AddPoint()
            {
                TotalPoints++;
            }
        }

        public override object GetFirstAnwer(string input)
        {
            List<Reindeer> reindeerData = new List<Reindeer>();
            foreach (var line in input.SplitLines())
            {
                reindeerData.Add(new Reindeer(line));
            }
            Parallel.ForEach(reindeerData, (a) => { a.RecalculateTraveledDistance(2503); });
            return reindeerData.Max(item => item.TotalTraveleDistance);
        }

        public override string GetQuestion1()
        {
            return DataClass.Day14;
        }

        public override object GetSecondAnwer(string input)
        {
            List<Reindeer> reindeerData = new List<Reindeer>();
            foreach (var line in input.SplitLines())
            {
                reindeerData.Add(new Reindeer(line));
            }
            for (int i = 1; i <= 2503; i++)
            {
                Parallel.ForEach(reindeerData, (a) => { a.RecalculateTraveledDistance(i); });
                reindeerData.Where(item => item.TotalTraveleDistance == reindeerData.Max(mitem => mitem.TotalTraveleDistance)).AsParallel().ForAll(item => item.AddPoint());
            }
            return reindeerData.Max(item => item.TotalPoints);
        }
    }
}
