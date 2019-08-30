using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;

namespace AtCoderWorkspace
{
    public class Solver
    {
        public void Input()
        {
            var cin = new Scanner();
            var s = cin.next();
           
            this.Solve();
        }

        public void Solve()
        {
            
        }
    }

    #region library

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

        public static long UpperBound(long[] a, long key)
        {
            long l = 0;
            long r = a.Length;
            long mid;
            while (l < r)
            {
                mid = ((r - l) / 2) + l;
                if (a[mid] < key)
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
       /// <param name="a">long[]</param>
       /// <returns></returns>
        public static long LIS(long[] a)
        {
            var dp = Enumerable.Range(0, a.Length).Select(x => long.MaxValue).ToArray();
            for (int i = 0; i < a.Length; i++)
            {
                dp[C.LowerBound(dp, a[i])] = a[i];
            }
            return C.LowerBound(dp, long.MaxValue-1);
        }

        /// <summary>
        /// 重複を許さない最長増加部分列
        /// </summary>
        /// <param name="a">long[]</param>
        /// <returns></returns>
        public static long UniqueLIS(long[] a)
        {
            var dp = Enumerable.Range(0, a.Length).Select(x => long.MaxValue).ToArray();
            for (int i = 0; i < a.Length; i++)
            {
                dp[C.UpperBound(dp, a[i])] = a[i];
            }
            return C.UpperBound(dp, long.MaxValue - 1);
        }
    }

    class PriorityQueue<T>
    {
        private readonly List<T> heap;
        private readonly Comparison<T> compare;
        private int size;
        public PriorityQueue() : this(Comparer<T>.Default) { }
        public PriorityQueue(IComparer<T> comparer) : this(16, comparer.Compare) { }
        public PriorityQueue(Comparison<T> comparison) : this(16, comparison) { }
        public PriorityQueue(int capacity, Comparison<T> comparison)
        {
            this.heap = new List<T>(capacity);
            this.compare = comparison;
        }
        public void Enqueue(T item)
        {
            this.heap.Add(item);
            var i = size++;
            while (i > 0)
            {
                var p = (i - 1) >> 1;
                if (compare(this.heap[p], item) <= 0)
                    break;
                this.heap[i] = heap[p];
                i = p;
            }
            this.heap[i] = item;

        }
        public T Dequeue()
        {
            var ret = this.heap[0];
            var x = this.heap[--size];
            var i = 0;
            while ((i << 1) + 1 < size)
            {
                var a = (i << 1) + 1;
                var b = (i << 1) + 2;
                if (b < size && compare(heap[b], heap[a]) < 0) a = b;
                if (compare(heap[a], x) >= 0)
                    break;
                heap[i] = heap[a];
                i = a;
            }
            heap[i] = x;
            heap.RemoveAt(size);
            return ret;
        }
        public T Peek() { return heap[0]; }
        public int Count { get { return size; } }
        public bool Any() { return size > 0; }
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

    public class ModInt
    {
        public static long Mod;

        public long num;

        public ModInt(long n, int mod = (int)1e9 + 7) { num = n; Mod = mod; }        
        public override string ToString() { return num.ToString(); }
        public static ModInt operator +(ModInt l, ModInt r) { l.num += r.num; if (l.num >= Mod) l.num -= Mod; return l; }
        public static ModInt operator -(ModInt l, ModInt r) { l.num -= r.num; if (l.num < 0) l.num += Mod; return l; }
        public static ModInt operator *(ModInt l, ModInt r) { return new ModInt(l.num * r.num % Mod); }
        public static implicit operator ModInt(long n) { n %= Mod; if (n < 0) n += Mod; return new ModInt(n); }
        public static ModInt Pow(ModInt v, long k) { return Pow(v.num, k); }
        public static ModInt Pow(long v, long k)
        {
            long ret = 1;
            for (k %= Mod - 1; k > 0; k >>= 1, v = v * v % Mod)
                if ((k & 1) == 1) ret = ret * v % Mod;
            return new ModInt(ret);
        }
        public static ModInt Inverse(ModInt v) { return Pow(v, Mod - 2); }
    }

    #endregion

    class Program
    {
        static void Main(string[] s) => new Solver().Input();
    }
}
