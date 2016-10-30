using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day4 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            return GetHexadecimalNumber(input, "00000");
        }

        public int GetHexadecimalNumber(string input, string searchPart)
        {
            MD5 md5Hash = MD5.Create();
            int number = 0;
            string format = "{0}{1}";
            for (int i = 0; i < 9999999; i++)
            {
                string code = string.Format(format, input, i);


                var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(code));
                var s = new StringBuilder();
                foreach (byte b in bytes)
                {
                    s.Append(b.ToString("x2"));
                }
                var hex = s.ToString();

               // Console.WriteLine(hex);
                if (hex.StartsWith(searchPart))
                {
                    return i;
                }
            }
            return number;
        }

        public override string GetQuestion1()
        {
            return DataClass.Day4;
        }

        public override object GetSecondAnwer(string input)
        {
            return GetHexadecimalNumber(input, "000000");
        }
    }
}
