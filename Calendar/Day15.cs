using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day15 : DayBase
    {
        class IngredientData
        {
            public IngredientData(string input)
            {
                var parts = input.Replace(":", "").Replace(",", "").Split(' ');
                Ingredient = parts[0];
                Capacity = Convert.ToInt16(parts[2]);
                Durability = Convert.ToInt16(parts[4]);
                Flavor = Convert.ToInt16(parts[6]);
                Texture = Convert.ToInt16(parts[8]);
                Calories = Convert.ToInt16(parts[10]);
            }

            public IngredientData(IngredientData data, int qty)
            {
                Ingredient = data.Ingredient;
                Capacity = data.Capacity;
                Durability = data.Durability;
                Flavor = data.Flavor;
                Texture = data.Texture;
                Calories = data.Calories;
                Qty = qty;
            }

            public string Ingredient { get; set; }
            public int Capacity { get; set; }
            public int Durability { get; set; }
            public int Flavor { get; set; }
            public int Texture { get; set; }
            public int Calories { get; set; }
            public int Qty { get; set; }

        }

        public override object GetFirstAnwer(string input)
        {
            return GetCookieMax(input);
        }

        public object GetCookieMax(string input, bool calcCalories = false)
        {
            List<IngredientData> allIngredients = new List<IngredientData>();
            List<IngredientData> mixedData = new List<IngredientData>();
            
            foreach (var item in input.SplitLines())
            {
                allIngredients.Add(new IngredientData(item));
            }
            var ingrCombination = allIngredients.GeneratePermutations();
            var ingCominaion = Enumerable.Range(1, 100).ToArray().GetCombinationsSortedByIndexSum(allIngredients.Count()).Where(sitem => sitem.Sum(sumItem => sumItem) == 100);
            int maxValue = 0;
            foreach (var qtyComb in ingCominaion)
            {
                foreach (var ingrc in ingrCombination)
                {
                    var listWithValues = new List<IngredientData>();
                    for (int i = 0; i < qtyComb.Count(); i++)
                    {
                        listWithValues.Add(new IngredientData(ingrc[i], qtyComb[i]));
                    }

                    var capacity = ConvertToRealQty(listWithValues.Sum(ingitem => ingitem.Capacity * ingitem.Qty));
                    var durability = ConvertToRealQty(listWithValues.Sum(ingitem => ingitem.Durability * ingitem.Qty));
                    var flavor = ConvertToRealQty(listWithValues.Sum(ingitem => ingitem.Flavor * ingitem.Qty));
                    var texture = ConvertToRealQty(listWithValues.Sum(ingitem => ingitem.Texture * ingitem.Qty));
                    var totalCalories = ConvertToRealQty(listWithValues.Sum(ingitem => ingitem.Calories * ingitem.Qty));
                    var score = capacity * durability * flavor * texture;
                    if (score > maxValue && ((!calcCalories) || (totalCalories == 500)) )
                    {
                        Console.WriteLine("{0}:{1}:{2}:{3}:{4}", score, listWithValues[0].Ingredient, listWithValues[0].Qty, listWithValues[1].Ingredient, listWithValues[1].Qty);
                        maxValue = score;
                    }
                }
            }
            return maxValue;
        }

        private int ConvertToRealQty(int qty)
        {
            if (qty < 0)
                return 0;
            else return qty;
        }

        public override string GetQuestion1()
        {
            return DataClass.Day15;
//            return @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
//Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";
        }

        public override object GetSecondAnwer(string input)
        {
            return GetCookieMax(input, true);
        }
    }
}
