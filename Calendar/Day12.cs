using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day12 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            JContainer myObject = JsonConvert.DeserializeObject<dynamic>(input);
            Console.WriteLine(myObject.GetType());
            return ReturnTreeSum(myObject, false);
        }

        public int ReturnTreeSum(JContainer container, bool ignoreRed)
        {
            int count = 0;
            if (container is JObject)
            {
                foreach (var item in (JObject)container)
                {
                    if (item.Value is JValue)
                    {
                        int newNumber = 0;
                        if (int.TryParse((item.Value as JValue).Value.ToString(), out newNumber))
                        {
                            count += newNumber;
                        }
                        else
                        {
                            if (ignoreRed && (item.Value.ToString() == "red"))
                                return 0;
                        }

                    }
                    else
                    {
                        count += ReturnTreeSum((JContainer)item.Value, ignoreRed);
                    }
                }
            }
            if (container is JArray)
            {
                foreach (var jcitem in (JArray)container)
                {
                    if (jcitem is JContainer)
                    {
                        count += ReturnTreeSum((JContainer)jcitem, ignoreRed);
                    }
                    else
                    {
                        int newNumber = 0;
                        if (int.TryParse(jcitem .Value<string>(), out newNumber))
                        {
                            count += newNumber;
                        }
                    }
                }
            }
            return count;
        }

        public override string GetQuestion1()
        {
            return File.ReadAllText("Day12.txt");
        }

        public override object GetSecondAnwer(string input)
        {
            JContainer myObject = JsonConvert.DeserializeObject<dynamic>(input);
            Console.WriteLine(myObject.GetType());
            return ReturnTreeSum(myObject, true); 
        }
    }
}
