using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public static class Utils
    {
        public static int CountMatrixSum(this int[,] matrix)
        {
            var arrSum =
                    (from int val in matrix
                     select val)
                     .Sum();
            return arrSum;
        }

        public static string[] SplitLines(this string input)
        {
            return input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }


        // Generate permutations.
        public static List<List<T>> GeneratePermutations<T>(this List<T> items)
        {
            // Make an array to hold the
            // permutation we are building.
            T[] current_permutation = new T[items.Count];

            // Make an array to tell whether
            // an item is in the current selection.
            bool[] in_selection = new bool[items.Count];

            // Make a result list.
            List<List<T>> results = new List<List<T>>();

            // Build the combinations recursively.
            PermuteItems<T>(items, in_selection,
                current_permutation, results, 0);

            // Return the results.
            return results;
        }

        // Recursively permute the items that are
        // not yet in the current selection.
        private static void PermuteItems<T>(List<T> items, bool[] in_selection,
            T[] current_permutation, List<List<T>> results,
            int next_position)
        {
            // See if all of the positions are filled.
            if (next_position == items.Count)
            {
                // All of the positioned are filled.
                // Save this permutation.
                results.Add(current_permutation.ToList());
            }
            else
            {
                // Try options for the next position.
                for (int i = 0; i < items.Count; i++)
                {
                    if (!in_selection[i])
                    {
                        // Add this item to the current permutation.
                        in_selection[i] = true;
                        current_permutation[next_position] = items[i];

                        // Recursively fill the remaining positions.
                        PermuteItems<T>(items, in_selection,
                            current_permutation, results,
                            next_position + 1);

                        // Remove the item from the current permutation.
                        in_selection[i] = false;
                    }
                }
            }
        }

        public static IEnumerable<IGrouping<TKey, TSource>> GroupAdjacent<TSource, TKey>(
           this IEnumerable<TSource> source,
           Func<TSource, TKey> keySelector)
        {
            TKey last = default(TKey);
            bool haveLast = false;
            List<TSource> list = new List<TSource>();
            foreach (TSource s in source)
            {
                TKey k = keySelector(s);
                if (haveLast)
                {
                    if (!k.Equals(last))
                    {
                        yield return new GroupOfAdjacent<TSource, TKey>(list, last);
                        list = new List<TSource>();
                        list.Add(s);
                        last = k;
                    }
                    else
                    {
                        list.Add(s);
                        last = k;
                    }
                }
                else
                {
                    list.Add(s);
                    last = k;
                    haveLast = true;
                }
            }
            if (haveLast)
                yield return new GroupOfAdjacent<TSource, TKey>(list, last);
        }

        public static IEnumerable<IEnumerable<T>> GetSubsets<T>(this IEnumerable<T> collection)
        {
            if (!collection.Any()) return Enumerable.Repeat(Enumerable.Empty<T>(), 1);
            var element = collection.Take(1);
            var ignoreFirstSubsets = GetSubsets(collection.Skip(1));
            var subsets = ignoreFirstSubsets.Select(set => element.Concat(set));
            return subsets.Concat(ignoreFirstSubsets);
        }

        // Given a pool of elements returns all the 
        // combinations of the groups of lenght r in pool, 
        // such that the combinations are ordered (ascending) by the sum of 
        // the indexes of the elements.
        // e.g. pool = {A,B,C,D,E} r = 3
        // returns
        // (A, B, C)   indexes: (0, 1, 2)   sum: 3
        // (A, B, D)   indexes: (0, 1, 3)   sum: 4
        // (A, B, E)   indexes: (0, 1, 4)   sum: 5
        // (A, C, D)   indexes: (0, 2, 3)   sum: 5
        // (A, C, E)   indexes: (0, 2, 4)   sum: 6
        // (B, C, D)   indexes: (1, 2, 3)   sum: 6
        // (A, D, E)   indexes: (0, 3, 4)   sum: 7
        // (B, C, E)   indexes: (1, 2, 4)   sum: 7
        // (B, D, E)   indexes: (1, 3, 4)   sum: 8
        // (C, D, E)   indexes: (2, 3, 4)   sum: 9
        public static IEnumerable<T[]>
        GetCombinationsSortedByIndexSum<T>(this IList<T> pool, int r)
        {
            int n = pool.Count;
            if (r > n)
                throw new ArgumentException("r cannot be greater than pool size");
            int minSum = F(r - 1);
            int maxSum = F(n) - F(n - r - 1);

            for (int sum = minSum; sum <= maxSum; sum++)
            {
                foreach (var indexes in AllSubSequencesWithGivenSum(0, n - 1, r, sum))
                    yield return indexes.Select(x => pool[x]).ToArray();
            }
        }


        // Given a start element and a last element of a sequence of consecutive integers
        // returns all the monotonically increasing subsequences of length "m" having sum "sum"
        // e.g. seqFirstElement = 1, seqLastElement = 5, m = 3, sum = 8
        //      returns {1,2,5} and {1,3,4}
        static IEnumerable<IEnumerable<int>>
        AllSubSequencesWithGivenSum(int seqFirstElement, int seqLastElement, int m, int sum)
        {
            int lb = sum - F(seqLastElement) + F(seqLastElement - m + 1);
            int ub = sum - F(seqFirstElement + m - 1) + F(seqFirstElement);

            lb = Math.Max(seqFirstElement, lb);
            ub = Math.Min(seqLastElement - m + 1, ub);

            for (int i = lb; i <= ub; i++)
            {
                if (m == 1)
                {
                    if (i == sum) // this check shouldn't be necessary anymore since LB/UB should automatically exclude wrong solutions
                        yield return new int[] { i };
                }
                else
                {
                    foreach (var el in AllSubSequencesWithGivenSum(i + 1, seqLastElement, m - 1, sum - i))
                        yield return new int[] { i }.Concat(el);
                }
            }
        }

        // Formula to compute the sum of the numbers from 0 to n
        // e.g. F(4) = 0 + 1 + 2 + 3 + 4 = 10
        static int F(int n)
        {
            return (n * (n + 1)) / 2;
        }

        public static IEnumerable<IEnumerable<int>> GetCombinationsBySum(this List<int> set, int sum, List<int> values)
        {
            for (int i = 0; i < set.Count(); i++)
            {
                int left = sum - set[i];
                //string vals = set[i] + "," + values;
                List<int> nValus = new List<int>(values);;
                nValus.Insert(0, set[i]);
                if (left == 0)
                {
                    yield return nValus;
                }
                else {
                    List<int> possible = new List<int>(set.Take(i).Where(n => n <= sum));
                    if (possible.Count() > 0)
                    {
                        foreach (List<int> s in possible.GetCombinationsBySum(left, nValus))
                        {
                            yield return s;
                        }
                    }
                }
            }
        }

        public static void PrintArray(this int[,] values)
        {
            Console.WriteLine("--------------");
            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int k = 0; k < values.GetLength(1); k++)
                {
                    Console.Write(values[i, k]);
                }

                Console.WriteLine();
            }
        }

        public static void PrintList<T>(this IEnumerable<T> values)
        {
            Console.WriteLine(String.Join(" ", values));
        }
    }

    public class GroupOfAdjacent<TSource, TKey> : IEnumerable<TSource>, IGrouping<TKey, TSource>
    {
        public TKey Key { get; set; }
        private List<TSource> GroupList { get; set; }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.Generic.IEnumerable<TSource>)this).GetEnumerator();
        }
        System.Collections.Generic.IEnumerator<TSource> System.Collections.Generic.IEnumerable<TSource>.GetEnumerator()
        {
            foreach (var s in GroupList)
                yield return s;
        }
        public GroupOfAdjacent(List<TSource> source, TKey key)
        {
            GroupList = source;
            Key = key;
        }
    }
}
