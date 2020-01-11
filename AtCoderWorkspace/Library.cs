using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Number = System.Int64;

namespace AtCoderWorkspace
{
    class Library
    {
    }

    class Modular
    {
        private const int M = 1000000007;
        public long value;
        public Modular(long value) { this.value = value; }
        public static implicit operator Modular(long a)
        {
            var m = a % M;
            return new Modular((m < 0) ? m + M : m);
        }
        public static Modular operator +(Modular a, Modular b)
        {
            return a.value + b.value;
        }
        public static Modular operator -(Modular a, Modular b)
        {
            return a.value - b.value;
        }
        public static Modular operator *(Modular a, Modular b)
        {
            return a.value * b.value;
        }
        private static Modular Pow(Modular a, int n)
        {
            switch (n)
            {
                case 0:
                    return 1;
                case 1:
                    return a;
                default:
                    var p = Pow(a, n / 2);
                    return p * p * Pow(a, n % 2);
            }
        }
        public static Modular operator /(Modular a, Modular b)
        {
            return a * Pow(b, M - 2);
        }
        private static readonly List<int> facs = new List<int> { 1 };
        public static Modular Fac(int n)
        {
            for (int i = facs.Count; i <= n; ++i)
            {
                facs.Add((int)(Math.BigMul(facs.Last(), i) % M));
            }
            return facs[n];
        }
        public static Modular Ncr(int n, int r)
        {
            return (n < r) ? 0
            : (n == r) ? 1
            : Fac(n) / (Fac(r) * Fac(n - r));
        }
        public static explicit operator int(Modular a)
        {
            return (int)a.value;
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

    public static partial class MathEx
    {
        public static int GCD(int n, int m) { return (int)GCD((long)n, m); }

        public static long GCD(long n, long m)
        {
            n = Math.Abs(n);
            m = Math.Abs(m);
            while (n != 0)
            {
                m %= n;
                if (m == 0) return n;
                n %= m;
            }
            return m;
        }

        public static long LCM(long n, long m) { return (n / GCD(n, m)) * m; }

        public static bool[] Sieve(int max, List<int> primes = null)
        {
            var isPrime = new bool[max + 1];
            for (int i = 2; i < isPrime.Length; i++) isPrime[i] = true;
            for (int i = 2; i * i <= max; i++)
                if (!isPrime[i]) continue;
                else for (int j = i * i; j <= max; j += i) isPrime[j] = false;
            if (primes != null) for (int i = 0; i <= max; i++) if (isPrime[i]) primes.Add(i);

            return isPrime;
        }
        public static bool IsPrime(int num)
        {
            if (num < 2) return false;
            else if (num == 2) return true;
            else if (num % 2 == 0) return false;

            double sqrtNum = Math.Sqrt(num);
            for (int i = 3; i <= sqrtNum; i += 2)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class Permutation
    {
        public IEnumerable<T[]> Enumerate<T>(IEnumerable<T> items)
        {
            return _GetPermutations<T>(new List<T>(), items.ToList());
        }

        private IEnumerable<T[]> _GetPermutations<T>(IEnumerable<T> perm, IEnumerable<T> items)
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

    public class Deque<T>
    {
        int dx;
        T[] buf;
        int mask;

        public int Count { get; private set; }

        public Deque() : this(8) { }

        public Deque(int capacity)
        {
            mask = capacity - 1;
            buf = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                return buf[(dx + index) & mask];
            }
            set
            {
                buf[(dx + index) & mask] = value;
            }
        }

        public void PushFront(T item)
        {
            if (Count == buf.Length) extend();
            dx = (dx + buf.Length - 1) & mask;
            buf[dx] = item;
            Count++;
        }

        public T PopFront()
        {
            var ret = buf[dx = (dx + 1) & mask];
            Count--;
            return ret;
        }

        public void PushBack(T item)
        {
            if (Count == buf.Length) extend();
            buf[(dx + Count++) & mask] = item;
        }

        public T PopBack()
        {
            var ret = buf[(dx + --Count) & mask];
            return ret;
        }

        public T Last()
        {
            return buf[(dx + Count - 1) & mask];
        }

        public T First()
        {
            return buf[dx];
        }

        void extend()
        {
            var nbuf = new T[buf.Length * 2];
            for (int i = 0; i < buf.Length; i++)
                nbuf[i] = buf[(dx + i) & mask];
            mask = mask * 2 + 1;
            dx = 0;
            buf = nbuf;
        }

        public void Clear() { Count = 0; }

        public bool Any() { return Count != 0; }

        public void Insert(int index, T item)
        {
            PushFront(item);
            for (int i = 0; i < index; i++)
                this[i] = this[i + 1];
            this[index] = item;
        }

        public T RemoveAt(int index)
        {
            var ret = this[index];
            for (int i = index; i > 0; i--)
                this[i] = this[i - 1];
            PopFront();
            return ret;
        }

        public T[] Items
        {
            get
            {

                var ret = new T[Count];
                for (int i = 0; i < Count; i++)
                    ret[i] = this[i];
                return ret;
            }
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
            return C.LowerBound(dp, long.MaxValue - 1);
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

    class DP
    {
        public static void ChMin(ref int a, int b)
        {
            long la = a;
            long lb = b;
            ChMin(ref la, lb);
        }

        public static bool ChMin(ref long a, long b)
        {
            if (a > b)
            {
                a = b;
                return true;
            }
            return false;
        }

        public static void ChMax(ref int a, int b)
        {
            long la = a;
            long lb = b;
            ChMax(ref la, lb);
        }

        public static bool ChMax(ref long a, long b)
        {
            if (a < b)
            {
                a = b;
                return true;
            }
            return false;
        }
    }
        
    public class FenwickTree
    {
        int n;
        Number[] bit;
        int max = 1;
        public FenwickTree(int size)
        {
            n = size; bit = new Number[n + 1];
            while ((max << 1) <= n) max <<= 1;
        }
        public Number this[int i, int j] { get { return this[j] - this[i - 1]; } }
        public Number this[int i] { get { Number s = 0; for (; i > 0; i -= i & -i) s += bit[i]; return s; } }
        public int LowerBound(Number w)
        {
            if (w <= 0) return 0;
            int x = 0;
            for (int k = max; k > 0; k >>= 1)
                if (x + k <= n && bit[x + k] < w)
                {
                    w -= bit[x + k];
                    x += k;
                }
            return x + 1;
        }
        public void Add(int i, Number v)
        {
            if (i == 0) System.Diagnostics.Debug.Fail("BIT is 1 indexed");
            for (; i <= n; i += i & -i) bit[i] += v;
        }
        public Number[] Items
        {
            get
            {
                var ret = new Number[n + 1];
                for (int i = 0; i < ret.Length; i++)
                    ret[i] = this[i, i];
                return ret;
            }
        }
    }
}
