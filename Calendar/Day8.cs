using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day8 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            var input4Work = input.Replace(Environment.NewLine, "");
            int totalSymbols = input4Work.Count();
            Console.WriteLine(input4Work);
            var resultInput = input4Work.Replace(@"\\", "S");
            Console.WriteLine(resultInput);
            resultInput = resultInput.Replace(@"\""", "B");
            Console.WriteLine(resultInput);
            resultInput = resultInput.Replace("\"", "");
            Console.WriteLine(resultInput);
            int hexadecimalCount = resultInput.Count(x => x == '\\');
            return totalSymbols - resultInput.Count() + (hexadecimalCount * 3);
        }

        public override string GetQuestion1()
        {
            return File.ReadAllText("Day8.txt");
        }

        public override object GetSecondAnwer(string input)
        {
            var input4Work = input.Replace(Environment.NewLine, "");
            int totalSymbols = input4Work.Count();
            input4Work = input;
            Console.WriteLine(input4Work);
            var resultInput = input4Work.Replace("\"\r\n", "YYY");
            resultInput = resultInput.Replace(@"\x", "AAA");
            Console.WriteLine(resultInput);
            resultInput = resultInput.Replace(@"\""", "BBBB");
            Console.WriteLine(resultInput);
            resultInput = resultInput.Replace(@"""", "KKK");
            Console.WriteLine(resultInput);
            resultInput = resultInput.Replace(@"\", "33");
            Console.WriteLine(resultInput);
            return resultInput.Replace(Environment.NewLine, "").Count() - totalSymbols;
        }
    }
}
