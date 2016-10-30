using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public abstract class DayBase
    {
        public abstract object GetFirstAnwer(string input);
        public abstract object GetSecondAnwer(string input);
        public abstract string GetQuestion1();

        public virtual string GetQuestion2()
        {
            return GetQuestion1();
        }
    }       
}
