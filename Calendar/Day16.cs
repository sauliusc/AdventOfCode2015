using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day16 : DayBase
    {
        class MachineData
        {
            public MachineData(string input)
            {
                var data = input.Replace(":", "").Replace(",", "").Split(' ');
                AuntNumber = data[1];
                UpdateData(data[2], data[3]);
                UpdateData(data[4], data[5]);
                UpdateData(data[6], data[7]);
            }

            public MachineData()
            {

            }

            public int? Children { get; set; }
            public int? Cats { get; set; }
            public int? Samoyeds { get; set; }
            public int? Pomeranians { get; set; }
            public int? Akitas { get; set; }
            public int? Vizslas { get; set; }
            public int? Trees { get; set; }
            public int? Cars { get; set; }
            public int? Perfumes { get; set; }
            public int? Goldfish { get; set; }
            public string AuntNumber { get; set; }

            private void UpdateData(string dataType, string counts)
            {
                int count = Convert.ToInt16(counts);
                switch (dataType)
                {
                    case "children":
                        Children = count;
                        break;
                    case "cats":
                        Cats = count;
                        break;
                    case "samoyeds":
                        Samoyeds = count;
                        break;
                    case "pomeranians":
                        Pomeranians = count;
                        break;
                    case "akitas":
                        Akitas = count;
                        break;
                    case "vizslas":
                        Vizslas = count;
                        break;
                    case "goldfish":
                        Goldfish = count;
                        break;
                    case "trees":
                        Trees = count;
                        break;
                    case "cars":
                        Cars = count;
                        break;
                    case "perfumes":
                        Perfumes = count;
                        break;
                    default:
                        break;

                }
            }

            public override bool Equals(object obj)
            {
                MachineData data = obj as MachineData;
                return (Children == null || data.Children == Children) &&
                    (Cats == null || data.Cats == Cats) &&
                    (Samoyeds == null || data.Samoyeds == Samoyeds) &&
                    (Pomeranians == null || data.Pomeranians == Pomeranians) &&
                    (Akitas == null || data.Akitas == Akitas) &&
                    (Vizslas == null || data.Vizslas == Vizslas) &&
                    (Goldfish == null || data.Goldfish == Goldfish) &&
                    (Trees == null || data.Trees == Trees) &&
                    (Cars == null || data.Cars == Cars) &&
                    (Perfumes == null || data.Perfumes == Perfumes);
            }

            public bool EqualByRetroencabulator(MachineData data)
            {
                return (Children == null || data.Children == Children) &&
                   
                    (Samoyeds == null || data.Samoyeds == Samoyeds) &&
                    
                    (Akitas == null || data.Akitas == Akitas) &&
                    (Vizslas == null || data.Vizslas == Vizslas) &&

                    (Goldfish == null || data.Goldfish > Goldfish) &&
                    (Pomeranians == null || data.Pomeranians > Pomeranians) &&

                    (Trees == null || data.Trees < Trees) &&
                    (Cats == null || data.Cats < Cats) &&

                    (Cars == null || data.Cars == Cars) &&
                    (Perfumes == null || data.Perfumes == Perfumes);
            }

        }

        public override object GetFirstAnwer(string input)
        {
            MachineData target = new MachineData() { Children = 3, Cats = 7, Samoyeds = 2, Pomeranians  = 3, Akitas = 0, Vizslas = 0, Goldfish = 5, Trees = 3, Cars = 2, Perfumes = 1};
            foreach (var item in input.SplitLines())
            {
                var newAunt = new MachineData(item);
                if (newAunt.Equals(target))
                {
                    return newAunt.AuntNumber;
                }
            }
            return "nothing";
        }

        public override string GetQuestion1()
        {
            return DataClass.Day16;
        }

        public override object GetSecondAnwer(string input)
        {
            MachineData target = new MachineData() { Children = 3, Cats = 7, Samoyeds = 2, Pomeranians = 3, Akitas = 0, Vizslas = 0, Goldfish = 5, Trees = 3, Cars = 2, Perfumes = 1 };
            foreach (var item in input.SplitLines())
            {
                var newAunt = new MachineData(item);
                if (newAunt.EqualByRetroencabulator(target))
                {
                    return newAunt.AuntNumber;
                }
            }
            return "nothing";
        }
    }
}
