using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderWorkspace.lib
{
    class Enumeration
    {
        // 約数列挙 Ο(√n)
        List<int> Divisor(int n)
        {
            var ret = new List<int>();
            for (int i = 1; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    ret.Add(i);
                    if (i != n / i) ret.Add(n / i);
                }
            }
            return ret;
        }

        // 素因数列挙 Ο(√n)
        public List<int> PrimeFactor(int n)
        {
            var ret = new List<int>();
            if (n == 1) { ret.Add(1); return ret; }
            if (n > 1)
            {
                for (int i = 2; i * i <= n; i++)
                {
                    while (n % i == 0)
                    {
                        ret.Add(i);
                        n /= i;
                    }
                }
                if (n != 1) ret.Add(n);
            }
            return ret;
        }

        // 順列列挙
        public static class Permutation
        {
            public static IEnumerable<T[]> Enumerate<T>(IEnumerable<T> items)
            {
                return _GetPermutations<T>(new List<T>(), items.ToList());
            }

            private static IEnumerable<T[]> _GetPermutations<T>(IEnumerable<T> perm, IEnumerable<T> items)
            {
                if (items.Count() == 0)
                {
                    yield return perm.ToArray();
                }
                else
                {
                    foreach (var item in items)
                    {
                        var result = _GetPermutations<T>(perm.Concat(new T[] { item }),
                                                            items.Where(x => x.Equals(item) == false)
                                        );
                        foreach (var xs in result)
                            yield return xs.ToArray();
                    }
                }
            }
        }

        // 組み合わせ列挙 withRepetition = true (重複組み合わせ) 
        public static class Combination
        {
            public static IEnumerable<T[]> Enumerate<T>(IEnumerable<T> items, int k, bool withRepetition)
            {
                if (k == 1)
                {
                    foreach (var item in items)
                        yield return new T[] { item };
                    yield break;
                }
                foreach (var item in items)
                {
                    var leftside = new T[] { item };

                    var unused = withRepetition ? items : items.SkipWhile(e => !e.Equals(item)).Skip(1).ToList();

                    foreach (var rightside in Enumerate(unused, k - 1, withRepetition))
                    {
                        yield return leftside.Concat(rightside).ToArray();
                    }
                }
            }
        }

    }
}
