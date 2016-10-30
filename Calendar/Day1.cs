using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day1 : DayBase
    {

        public override object GetFirstAnwer(string input)
        {
            throw new NotImplementedException();
        }

        public override string GetQuestion1()
        {
            return DataClass.Day1;
        }

        public override object GetSecondAnwer(string input)
        {
            int floor = 0;
            int count = 1;
            foreach (var item in input)
            {
                if (item == '(')
                {
                    floor += 1;
                }
                else
                {
                    floor -= 1;
                }
                if (floor == -1)
                    return count;
                count++;
            }
            return count;
        }
    }
}
