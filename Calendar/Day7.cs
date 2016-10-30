using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class Day7 : DayBase
    {
        private static List<QuestionItemBase> _questionItems = new List<QuestionItemBase>();

        public class ValueKey
        {
            public ValueKey(string key, uint value)
            {
                Key = key;
                Value = value;
            }
            public uint Value { get; set; }
            public string Key { get; set; }
            public bool HasValue()
            {
                if (Value == 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }

        abstract class QuestionItemBase
        {
            string _original;
            public QuestionItemBase(string original)
            {
                _original = original;
            }

            public Operations Operation { get; set; }
            public ValueKey Value1 { get; set; }
            public ValueKey Value2 { get; set; }
            public ValueKey Result { get; set; }

            public bool Parsed { get; set; }
            public uint GetBitwiseResult()
            {
                if (!Parsed)
                {
                    EvulateBitwiseResult();
                    Console.WriteLine("{0}: {1}, {2}, {3}, {4}", _original, Enum.GetName(typeof(Operations), Operation), Value1.Value, Value2== null? "": Value2.Value.ToString(), Result.Value);
                    Parsed = true;
                }
                return this.Result.Value;
            }
            public abstract void EvulateBitwiseResult();
        }

        class QuestionAssig: QuestionItemBase
        {
            public QuestionAssig(string[] splittedItems)
                :base(String.Join(" ", splittedItems))
            {
                base.Operation = Operations.Assign;
                uint parsedValue = 0;
                if (uint.TryParse(splittedItems[0], out parsedValue))
                {
                    base.Value1 = new ValueKey("", parsedValue);
                    base.Result = new ValueKey(splittedItems[2], parsedValue);
                    this.Parsed = true;
                }
                else
                {
                    base.Value1 = new ValueKey(splittedItems[0], 0);
                    base.Result = new ValueKey(splittedItems[2], 0);
                }
            }

            public override void EvulateBitwiseResult()
            {
                var key = _questionItems.FirstOrDefault(item => item.Result.Key == this.Value1.Key).GetBitwiseResult();
                base.Result.Value = key;
            }
        }

        class QuestionNot : QuestionItemBase
        {
            public QuestionNot(string[] splittedItems)
                : base(String.Join(" ", splittedItems))
            {
                base.Operation = Operations.Not;
                Value1 = new ValueKey(splittedItems[1], 0);
                Result = new ValueKey(splittedItems[3], 0);
            }

            public override void EvulateBitwiseResult()
            {
                var key = _questionItems.FirstOrDefault(item => item.Result.Key == this.Value1.Key).GetBitwiseResult();
                base.Result.Value = 65535 - key;
            }
        }

        class QuestionAnd : QuestionItemBase
        {
            public QuestionAnd(string[] splittedItems)
                : base(String.Join(" ", splittedItems))
            {
                base.Operation = Operations.And;
                uint parsedValue = 0;
                if (uint.TryParse(splittedItems[0], out parsedValue))
                {
                    base.Value1 = new ValueKey("", parsedValue);
                }
                else
                {
                    base.Value1 = new ValueKey(splittedItems[0], 0);
                }
                if (uint.TryParse(splittedItems[2], out parsedValue))
                {
                    base.Value2 = new ValueKey("", parsedValue);
                }
                else
                {
                    base.Value2 = new ValueKey(splittedItems[2], 0);
                }
                Result = new ValueKey(splittedItems[4], 0);
            }

            public override void EvulateBitwiseResult()
            {
                var key1 = this.Value1.Key == ""? this.Value1.Value: _questionItems.FirstOrDefault(item => item.Result.Key == this.Value1.Key).GetBitwiseResult();
                var key2 = this.Value2.Key == "" ? this.Value2.Value : _questionItems.FirstOrDefault(item => item.Result.Key == this.Value2.Key).GetBitwiseResult();
                base.Result.Value = (key1 & key2);
            }
        }

        class QuestionOr : QuestionItemBase
        {
            public QuestionOr(string[] splittedItems)
                : base(String.Join(" ", splittedItems))
            {
                base.Operation = Operations.Or;
                base.Value1 = new ValueKey(splittedItems[0], 0);
                base.Value2 = new ValueKey(splittedItems[2], 0);
                Result = new ValueKey(splittedItems[4], 0);
            }

            public override void EvulateBitwiseResult()
            {
                var key1 = this.Value1.Key == "" ? this.Value1.Value : _questionItems.FirstOrDefault(item => item.Result.Key == this.Value1.Key).GetBitwiseResult();
                var key2 = this.Value2.Key == "" ? this.Value2.Value : _questionItems.FirstOrDefault(item => item.Result.Key == this.Value2.Key).GetBitwiseResult();
                base.Result.Value = (key1 | key2);
            }
        }

        class QuestionRShift : QuestionItemBase
        {
            public QuestionRShift(string[] splittedItems)
                : base(String.Join(" ", splittedItems))
            {
                base.Operation = Operations.RShift;
                base.Value1 = new ValueKey(splittedItems[0], 0);
                base.Value2 = new ValueKey("", Convert.ToUInt16(splittedItems[2]));
                Result = new ValueKey(splittedItems[4], 0);
            }

            public override void EvulateBitwiseResult()
            {
                var key1 = _questionItems.FirstOrDefault(item => item.Result.Key == this.Value1.Key).GetBitwiseResult();
                base.Result.Value = key1 >> Convert.ToByte(Value2.Value);
            }
        }

        class QuestionLShift : QuestionItemBase
        {
            public QuestionLShift(string[] splittedItems)
                : base(String.Join(" ", splittedItems))
            {
                base.Operation = Operations.LShift;
                base.Value1 = new ValueKey(splittedItems[0], 0);
                base.Value2 = new ValueKey("", Convert.ToUInt16(splittedItems[2]));
                Result = new ValueKey(splittedItems[4], 0);
            }

            public override void EvulateBitwiseResult()
            {
                var key1 = _questionItems.FirstOrDefault(item => item.Result.Key == this.Value1.Key).GetBitwiseResult();
                base.Result.Value = key1 << Convert.ToByte(Value2.Value);
            }
        }

        enum Operations
        {
            And,
            Or,
            LShift,
            RShift,
            Not,
            Assign
        }

        //uint x = 123;
        //uint y = 456;
        //uint d = (x & y); //and
        //uint e = (x | y); //or
        //uint f = x << 2;  //lshift
        //uint g = y >> 2;  //rshift
        //uint h = NotFunction(x);  //not

        public override object GetFirstAnwer(string input)
        {
            _questionItems = new List<QuestionItemBase>();
            foreach (var line in input.SplitLines())
            {
                var splitedItems = line.Split(' ');
                if (splitedItems.Count() == 3)
                {
                    _questionItems.Add(new QuestionAssig(splitedItems));
                } else if (splitedItems.Count() == 4)
                {
                    _questionItems.Add(new QuestionNot(splitedItems));
                }
                else
                {
                    switch (splitedItems[1])
                    {
                        case "AND":
                            _questionItems.Add(new QuestionAnd(splitedItems));
                            break;
                        case "OR":
                            _questionItems.Add(new QuestionOr(splitedItems));
                            break;
                        case "LSHIFT":
                            _questionItems.Add(new QuestionLShift(splitedItems));
                            break;
                        case "RSHIFT":
                            _questionItems.Add(new QuestionRShift(splitedItems));
                            break;
                        default:
                            break;
                    }
                }
            }
            return _questionItems.FirstOrDefault(item => item.Result.Key == "a").GetBitwiseResult();

        }

        public override object GetSecondAnwer(string input)
        {
            return GetFirstAnwer(GetQuestion2().Replace("1674 -> b", "46065 -> b"));
        }

        public override string GetQuestion1()
        {
            return DataClass.Day7;
        }
    }
}
