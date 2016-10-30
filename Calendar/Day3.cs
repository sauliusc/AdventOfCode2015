using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day3 : DayBase
    {
        public override object GetFirstAnwer(string input)
        {
            int[,] locationmap = new int[1000, 1000];
            int x = 500;
            int y = 500;
            locationmap[x, y] = 1;
            foreach (var character in input)
            {
                switch (character)
                {
                    case '^':
                        y++;
                        break;
                    case 'v':
                        y--;
                        break;
                    case '>':
                        x++;
                        break;
                    case '<':
                        x--;
                        break;
                    default:
                        break;
                }
                locationmap[x, y] = 1;

            }
            //locationmap.OfType<int>()
            var arrSum =
                    (from int val in locationmap
                     select val)
                     .Sum();
            return arrSum;
        }

        public override string GetQuestion1()
        {
            return DataClass.Day3;
        }

        public override object GetSecondAnwer(string input)
        {
            int[,] locationmap = new int[1000, 1000];
            int x = 500;
            int y = 500;
            int xx = 500;
            int yy = 500;
            int count = 1;
            locationmap[x, y] = 2;
            foreach (var character in input)
            {
                bool santa = (count % 2 == 1);
                if (santa)
                {
                    switch (character)
                    {
                        case '^':
                            y++;
                            break;
                        case 'v':
                            y--;
                            break;
                        case '>':
                            x++;
                            break;
                        case '<':
                            x--;
                            break;
                        default:
                            break;
                    }
                    locationmap[x, y] = 1;
                }
                else
                {
                    switch (character)
                    {
                        case '^':
                            yy++;
                            break;
                        case 'v':
                            yy--;
                            break;
                        case '>':
                            xx++;
                            break;
                        case '<':
                            xx--;
                            break;
                        default:
                            break;
                    }
                    locationmap[xx, yy] = 1;
                }
                count++;

            }
            //locationmap.OfType<int>()
            var arrSum =
                    (from int val in locationmap
                     select val)
                     .Sum();
            return arrSum;
        }
    }
}
