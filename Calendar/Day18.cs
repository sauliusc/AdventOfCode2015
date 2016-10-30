using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day18 : DayBase
    {
        private int _matrixSize = 100;
        private int _iterationCount = 100;
        public object GerLightCount(string input, bool isCornerOn)
        {
            int[,] lights = new int[_matrixSize+2, _matrixSize + 2];
            int x = 1;
            int y = 1;
            foreach (var line in input.SplitLines())
            {
                x = 1;
                foreach (var symbol in line)
                {
                    lights[x, y] = SymbolToLight(symbol);
                    x++;
                }
                y++;
            }
            if (isCornerOn)
                LightCornerOn(ref lights);
            for (int i = 0; i < _iterationCount; i++)
            {
                lights = GetNewArray(lights);
                if (isCornerOn)
                    LightCornerOn(ref lights);
            }
            return lights.CountMatrixSum();
        }

        private void LightCornerOn(ref int[,] oldArray)
        {
            oldArray[1, 1] = oldArray[1, _matrixSize] = oldArray[_matrixSize, 1] = oldArray[_matrixSize, _matrixSize] = 1;
        }

        private int[,] GetNewArray(int[,] oldArray)
        {
            int[,] newArray = new int[_matrixSize+2, _matrixSize+2];
            for (int x = 1; x <= _matrixSize; x++)
                for (int y = 1; y <= _matrixSize; y++)
                {
                    var lightCount =
                        oldArray[x - 1, y - 1] +
                        oldArray[x, y - 1] +
                        oldArray[x + 1, y - 1] +
                        oldArray[x - 1, y] +
                        oldArray[x + 1, y] +
                        oldArray[x - 1, y + 1] +
                        oldArray[x, y + 1] +
                        oldArray[x + 1, y + 1];
                    newArray[x, y] = GetLightState(oldArray[x, y], lightCount);
                }
            return newArray;
        }

        private int GetLightState(int state, int lightCount)
        {
            if ((state == 1 && lightCount >= 2 && lightCount <= 3) || (state == 0 && lightCount == 3))
                    return 1;
            return 0;
        }

        private int SymbolToLight(char symbol)
        {
            if (symbol == '#')
                return 1;
            return 0;
        }

        public override string GetQuestion1()
        {
//            return @".#.#.#
//...##.
//#....#
//..#...
//#.#..#
//####..";
           return DataClass.Day18;
        }

        public override object GetFirstAnwer(string input)
        {
            return GerLightCount(input, false);
        }

        public override object GetSecondAnwer(string input)
        {
            return GerLightCount(input, true);
        }
    }
}
