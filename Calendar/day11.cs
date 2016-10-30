using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day11 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            List<string> badLetters = new List<string>() { "i", "o", "l" };
            List<string> allPosibleSeq = new List<string>();
            for (int i = 65; i <= 90-2; i++)
            {
                allPosibleSeq.Add( string.Join("", new char[] { (char)i, (char)(i+1), (char)(i+2) }).ToLower());
            }
            bool skipBegin = true;
            for (char c1 = 'a'; c1 <= 'z'; c1++)
            {
                if (skipBegin)
                    c1 = input[0];
                for (char c2 = 'a'; c2 <= 'z'; c2++)
                {
                    if (skipBegin)
                        c2 = input[1];
                    for (char c3 = 'a'; c3 <= 'z'; c3++)
                    {
                        if (skipBegin)
                            c3 = input[2];
                        for (char c4 = 'a'; c4 <= 'z'; c4++)
                        {
                            if (skipBegin)
                                c4 = input[3];
                            for (char c5 = 'a'; c5 <= 'z'; c5++)
                            {
                                if (skipBegin)
                                    c5 = input[4];
                                for (char c6 = 'a'; c6 <= 'z'; c6++)
                                {
                                    if (skipBegin)
                                        c6 = input[5];
                                    for (char c7 = 'a'; c7 <= 'z'; c7++)
                                    {
                                        if (skipBegin)
                                            c7 = input[6];
                                        for (char c8 = 'a'; c8 <= 'z'; c8++)
                                        {
                                            if (skipBegin)
                                            {
                                                c8 = input[7];
                                                skipBegin = false;
                                            }
                                            var result = string.Join("", new char[] { c1, c2, c3, c4, c5, c6, c7, c8 });
                                            if (result != input)
                                                if (!badLetters.Any(s => result.Contains(s)))
                                                {
                                                    var group = result.GroupAdjacent(i => i);
                                                    if (group.Count(item => item.Count() > 1) > 1)
                                                    {
                                                        if (allPosibleSeq.Any(s => result.Contains(s)))
                                                        {
                                                            return result;
                                                        }
                                                    }
                                                }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return 0;
        }

        public override string GetQuestion1()
        {
            return "vzbxkghb";
        }
        public override string GetQuestion2()
        {
            //vzcxxyzz blogas
            return "vzbxxyzz";
        }

        public override object GetSecondAnwer(string input)
        {
            return GetFirstAnwer(input);
        }
    }
}
