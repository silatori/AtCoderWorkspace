using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;

namespace AtCoderWorkspace
{
    class Program
    {
        static void Main(string[] args)
        {
            var cin = new Scanner();
            var s = cin.next();

            Console.WriteLine();
        }
    }

    static class C
    {
        public static long LowerBound(long[] a, long key)
        {
            long l = 0;
            long r = a.Length;
            long mid;
            while (l < r)
            {
                mid = ((r - l) / 2) + l;
                if (a[mid] <= key)
                    l = mid + 1;
                else
                    r = mid;
            }
            return l;
        }
    }

    static class Subsequence
    {
        /// <summary>
        /// 最長増加部分列
        /// </summary>
        public static long LIS(long[] a)
        {
            var dp = Enumerable.Range(0, a.Length).Select(x => long.MaxValue).ToArray();
            for (int i = 0; i < a.Length; i++)
            {
                dp[C.LowerBound(dp, a[i])] = a[i];
            }
            return C.LowerBound(dp, long.MaxValue-1);
        }
    }

    class Scanner
    {
        string[] s;
        int i;

        char[] cs = new char[] { ' ' };

        public Scanner()
        {
            s = new string[0];
            i = 0;
        }

        public string next()
        {
            if (i < s.Length) return s[i++];
            string st = Console.ReadLine();
            while (st == "") st = Console.ReadLine();
            s = st.Split(cs, StringSplitOptions.RemoveEmptyEntries);
            if (s.Length == 0) return next();
            i = 0;
            return s[i++];
        }

        public int nextInt()
        {
            return int.Parse(next());
        }
        public int[] ArrayInt(int N, int add = 0)
        {
            int[] Array = new int[N];
            for (int i = 0; i < N; i++)
            {
                Array[i] = nextInt() + add;
            }
            return Array;
        }

        public long nextLong()
        {
            return long.Parse(next());
        }

        public long[] ArrayLong(int N, long add = 0)
        {
            long[] Array = new long[N];
            for (int i = 0; i < N; i++)
            {
                Array[i] = nextLong() + add;
            }
            return Array;
        }

        public double nextDouble()
        {
            return double.Parse(next());
        }


        public double[] ArrayDouble(int N, double add = 0)
        {
            double[] Array = new double[N];
            for (int i = 0; i < N; i++)
            {
                Array[i] = nextDouble() + add;
            }
            return Array;
        }
    }
}
