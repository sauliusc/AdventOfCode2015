using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day5 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            int wordCount = 0;
            char[] vovels = new char[] { 'a', 'e', 'i', 'o', 'u' };
            List<string> badLetters = new List<string>() { "ab", "cd", "pq", "xy" };
            foreach (var line in input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if (!badLetters.Any(s => line.Contains(s)))
                {
                    if (line.Count(s => vovels.Any(ss => ss == s)) >= 3)
                    {
                        int longestRun = line.Select((c, i) => line.Substring(i).TakeWhile(x => x == c).Count()).Max();
                        if (longestRun > 1)
                            wordCount++;
                    }
                }


            }
            return wordCount;
        }

        public override string GetQuestion1()
        {
            return DataClass.Day5;
        }

        public override object GetSecondAnwer(string input)
        {
            int wordCount = 0;
            foreach (var line in input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                for (int i = 0; i < line.Count() - 2; i++)
                {
                    if (line[i] == line[i + 2])
                    {
                        for (int ii = 0; ii < line.Count() - 1; ii++)
                        {
                            if (line.Substring(ii + 2).Contains(line.Substring(ii, 2)))
                            {
                                wordCount++;
                                break;
                            }
                        }
                        break;
                    }
                }

            }
            return wordCount;
        }
    }
}
