using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day13 : DayBase
    {
        public struct GuestValue
        {
            public GuestValue(string guest1, string guest2, string hapiness)
            {
                Guest1 = guest1;
                Guest2 = guest2;
                Hapiness = Convert.ToInt32(hapiness);
            }
            
            public string Guest1 { get; set; }
            public string Guest2 { get; set; }
            public int Hapiness { get; set; }

            public bool EqualGuests(string guest1, string guest2)
            {
                return (guest1 == Guest1 && guest2 == Guest2);
            }
        }

        public override object GetFirstAnwer(string input)
        {
            List<string> allGuests = new List<string>();
            List<GuestValue> guestData = new List<GuestValue>();
            ReadData(input, allGuests, guestData);
            return GetHapinessResult(allGuests, guestData);
        }

        public override object GetSecondAnwer(string input)
        {
            List<string> allGuests = new List<string>();
            List<GuestValue> guestData = new List<GuestValue>();
            ReadData(input, allGuests, guestData);
            allGuests.Add("SANTA");
            foreach (var guest in allGuests)
            {
                guestData.Add(new GuestValue(guest, "SANTA", "0"));
                guestData.Add(new GuestValue("SANTA", guest, "0"));
            }
            return GetHapinessResult(allGuests, guestData);
            //return GetHapinessResult(input, 0, (totalDistance, result) => { return totalDistance > result; });
        }

        public void ReadData(string input, List<string> allGuests, List<GuestValue> guestData)
        {
            foreach (var line in input.SplitLines())
            {
                var items = line.TrimEnd('.').Split(' ');
                if (!allGuests.Contains(items[0]))
                    allGuests.Add(items[0]);
                if (!allGuests.Contains(items[10]))
                    allGuests.Add(items[10]);
                guestData.Add(new GuestValue(items[0], items[10], items[2] == "gain" ? items[3] : string.Format("-{0}", items[3])));
            }
        }

        public int GetHapinessResult(List<string> allGuests, List<GuestValue> guestData)
        {
            int result = int.MinValue;
            List<List<string>> allPosibleValues = allGuests.GeneratePermutations();
            foreach (var combination in allPosibleValues)
            {
                int totalHapiness = 0;
                for (int i = 0; i < combination.Count() - 1; i++)
                {
                    totalHapiness += guestData.Where(item => item.EqualGuests(combination[i], combination[i + 1]) || item.EqualGuests(combination[i+1], combination[i])).Sum(sitem => sitem.Hapiness);
                }
                totalHapiness += guestData.Where(item => item.EqualGuests(combination[0], combination[combination.Count()-1]) || item.EqualGuests(combination[combination.Count() - 1], combination[0])).Sum(sitem => sitem.Hapiness);
                if (totalHapiness > result)
                {
                    result = totalHapiness;
                }
            }

            return result;
        }

        public override string GetQuestion1()
        {
            return DataClass.Day13;
        }

        

    }
}
