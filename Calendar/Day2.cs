using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day2 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int area = 0;
            foreach (var line in lines)
            {
                var splittedItems = line.Split('x');
                int length = Convert.ToInt16(splittedItems[0]);
                int width = Convert.ToInt16(splittedItems[1]);
                int height = Convert.ToInt16(splittedItems[2]);
                int part1 = length * width;
                int part2 = width * height;
                int part3 = height * length;
                int minArea = Math.Min(Math.Min(part1, part2), part3);
                area += ((part1 + part2 + part3) * 2) + minArea;
            }
            return area;
        } 

        public override string GetQuestion1()
        {
            return DataClass.Day2;
        }

        public override object GetSecondAnwer(string input)
        {
            var lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int area = 0;
            foreach (var line in lines)
            {
                var splittedItems = line.Split('x');
                int length = Convert.ToInt16(splittedItems[0]);
                int width = Convert.ToInt16(splittedItems[1]);
                int height = Convert.ToInt16(splittedItems[2]);
                int maxLength = Math.Max(Math.Max(length, width), height);
                area += ((length + width + height - maxLength) * 2) + (length * width * height);
            }
            return area;
        }
    }
}
