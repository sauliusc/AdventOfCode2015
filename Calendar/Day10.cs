using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day10 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            string resultString = input;
            for (int i = 0; i <40; i++)
            {
                resultString = GetLookSay(resultString);
                Console.WriteLine("{0}:{1}", DateTime.Now.ToLongTimeString(), i);
            }
            return resultString.Length;
        }

        private string GetLookSay(string input)
        {
            var result = input.ToCharArray().ToList<char>().GroupAdjacent(i => i);
            StringBuilder sb = new StringBuilder();
            foreach (var item in result)
            {
                sb.Append(item.Count());
                sb.Append(item.Key);
            }
            return sb.ToString();
        }

        public override string GetQuestion1()
        {
            return DataClass.Day10;
        }

        public override object GetSecondAnwer(string input)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string resultString = input;
            for (int i = 0; i < 50; i++)
            {
                resultString = GetLookSay(resultString);
                Console.WriteLine("{0}:{1}", DateTime.Now.ToLongTimeString(), i);
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return resultString.Length;
        }


    }

    
    public static class LocalExtensions
    {
       
    }
}
