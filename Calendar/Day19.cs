using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day19 : DayBase
    {
        const string _assign = " => ";

        private string _molecule;
        List<Replacement> _rules = new List<Replacement>();

        public struct Replacement
        {
            public Replacement(string input)
            {
                var items = input.Replace(_assign, ".").Split('.');
                Source = items[0];
                Destination = items[1];
            }
            public string Source { get; set; }
            public string Destination { get; set; }
        }

        public override object GetFirstAnwer(string input)
        {
            List<Replacement> rules = new List<Replacement>();
            List<string> variants = new List<string>();
            string molecule = string.Empty;
            foreach(var item in input.SplitLines())
            {
                if (item.Contains(_assign))
                    rules.Add(new Replacement(item));
                else
                    molecule = item;
            }
            for (int i = 0; i < molecule.Length; i++)
            {
                foreach (var item in rules.Where(s => molecule.Substring(i, molecule.Length - i).StartsWith(s.Source)))
                {
                    variants.Add(molecule.Remove(i, item.Source.Length).Insert(i, item.Destination));
                }
            }
            return variants.Distinct().Count();
        }

        public override string GetQuestion1()
        {
            return DataClass.Day19;
            return @"e => H
e => O
H => HO
H => OH
O => HH
HOHOHO";

        }

        public override object GetSecondAnwer(string input)
        {
            _failed = new List<string>();
            List<string> variants = new List<string>();
            foreach (var item in input.SplitLines())
            {
                if (item.Contains(_assign))
                    _rules.Add(new Replacement(item));
                else
                    _molecule = item;
            };
            int countOnFound = int.MaxValue;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ResultFound("e", ref countOnFound, 0);
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            return countOnFound;
        }

        private bool ResultFound(string input, ref int countOnFound, int iterationCount)
        {
            iterationCount++;
            //Console.WriteLine("iteration:{0} length:{1}", iterationCount, input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                foreach (var item in _rules.Where(s => input.Substring(i, input.Length - i).StartsWith(s.Source)))
                {
                    var result = new StringBuilder(input).Remove(i, item.Source.Length).Insert(i, item.Destination).ToString();
                    if (result == _molecule)
                    {
                        if (countOnFound > iterationCount)
                        {
                            countOnFound = iterationCount;
                            return true;
                        }
                        else return false;
                    }
                    else if (result.Length < _molecule.Length)
                    {
                        if (!_failed.Contains(result))
                        {
                            ResultFound(result, ref countOnFound, iterationCount);
                        }
                    }
                    else
                    {
                        _failed.Add(input);
                        if (result == debug)
                        {

                        }
                        //File.AppendAllText("Day19.txt", result); File.AppendAllText("Day19.txt", Environment.NewLine);
                    };
                }
            }
            return true;
        }

        List<string> _failed = new List<string>();

        string debug = "CRnThPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBCaPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBPBFArF";

        //parallel
        //public override object GetSecondAnwer(string input)
        //{

        //    List<string> variants = new List<string>();
        //    foreach (var item in input.SplitLines())
        //    {
        //        if (item.Contains(_assign))
        //            _rules.Add(new Replacement(item));
        //        else
        //            _molecule = item;
        //    };
        //    int countOnFound = int.MaxValue;
        //    Stopwatch sw = new Stopwatch();
        //    sw.Start();
        //    ResultFound("e", ref countOnFound, 0);
        //    sw.Stop();
        //    Console.WriteLine(sw.ElapsedMilliseconds);
        //    return countOnFound;
        //}

        //private bool ResultFound(string input, ref int countOnFound, int iterationCount)
        //{
        //    iterationCount++;
        //    var items = Enumerable.Range(0, input.Length).Select(index => new { index, input});
        //    Parallel.ForEach(items, new ParallelOptions { MaxDegreeOfParallelism = 500 }, (inputData) =>
        //    {

        //        foreach (var item in _rules.Where(s => input.Substring(inputData.index, inputData.input.Length - inputData.index).StartsWith(s.Source)))
        //        {
        //            var result = input.Remove(inputData.index, item.Source.Length).Insert(inputData.index, item.Destination);
        //            if (result == _molecule)
        //            {
        //                //if (countOnFound > iterationCount)
        //                //{
        //                //    countOnFound = iterationCount;
        //                //    return true;
        //                //}
        //                //else return false;
        //            }
        //            else if (result.Length < _molecule.Length)
        //            {
        //                int value = 0;
        //                ResultFound(result, ref value, iterationCount);
        //            }
        //        }

        //    });


        //    return true;
        //}

    }
}
