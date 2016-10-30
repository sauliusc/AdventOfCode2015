using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day6 : DayBase
    {
        const string _turnOn = "turn on ";
        const string _turnOff = "turn off ";
        const string _toggle = "toggle ";
        public int GetLightsCount(string input, bool withBrightness = false)
        {
            int[,] lights = new int[1000, 1000];
            foreach (var line in input.SplitLines())
            {
                string stringToWork = line;
                Mode light = GetLightMode(ref stringToWork);
                var variables = stringToWork.Split(' ');
                var startpoints = variables[0].Split(',');
                var endpoints = variables[2].Split(',');
                switch(light)
                {
                    case Mode.TurnOn:
                        TurnOn(lights, Convert.ToInt16(startpoints[0]), Convert.ToInt16(startpoints[1]), Convert.ToInt16(endpoints[0]), Convert.ToInt16(endpoints[1]), withBrightness);
                    break;
                    case Mode.TurnOff:
                        TurnOff(lights, Convert.ToInt16(startpoints[0]), Convert.ToInt16(startpoints[1]), Convert.ToInt16(endpoints[0]), Convert.ToInt16(endpoints[1]), withBrightness);
                        break;
                    case  Mode.Toggle:
                        Toggle(lights, Convert.ToInt16(startpoints[0]), Convert.ToInt16(startpoints[1]), Convert.ToInt16(endpoints[0]), Convert.ToInt16(endpoints[1]), withBrightness);
                        break;
                    default:
                        break;
                }
            }
            return lights.CountMatrixSum();
        }

        private void Toggle(int[,] lights, short startx, short starty, short endx, short endy, bool withBrightness = false)
        {
            for (int x = startx; x <= endx; x++)
                for (int y = starty; y <= endy; y++)
                {
                    if (withBrightness)
                    {
                        lights[x, y] += 2;
                    }
                    else
                    {
                        if (lights[x, y] == 0)
                            lights[x, y] = 1;
                        else lights[x, y] = 0;
                    }
                }
        }

        private void TurnOff(int[,] lights, short startx, short starty, short endx, short endy, bool withBrightness = false)
        {
            for (int x = startx; x <= endx; x++)
                for (int y = starty; y <= endy; y++)
                {
                    if (withBrightness)
                    {
                        if (lights[x, y] > 0)
                        {
                            lights[x, y]--;
                        }
                        
                    }
                    else
                    {
                        lights[x, y] = 0;
                    }
                }
        }

        private void TurnOn(int[,] lights, short startx, short starty, short endx, short endy, bool withBrightness = false)
        {
            for (int x = startx; x <= endx; x++)
                for (int y = starty; y <= endy; y++)
                {
                    if(withBrightness)
                    {
                        lights[x, y]++;
                    }
                    else
                    {
                        lights[x, y] = 1;
                    }
                        
                }
        }

        private Mode GetLightMode(ref string line)
        {
            if (line.StartsWith(_turnOn))
            {
                line = line.Replace(_turnOn, "");
                return Mode.TurnOn;
            }
            else if (line.StartsWith(_turnOff))
            {
                line = line.Replace(_turnOff, "");
                return Mode.TurnOff;
            }
            else
            {
                line = line.Replace(_toggle, "");
                return Mode.Toggle;
            }
        }

        public override object GetFirstAnwer(string input)
        {
            return GetLightsCount(input, false);
        }

        public override object GetSecondAnwer(string input)
        {
            return GetLightsCount(input, true);
        }

        public override string GetQuestion1()
        {
            return DataClass.Day6;
        }

        enum Mode
        {
            TurnOn,
            TurnOff,
            Toggle
        }
    }

    
}
