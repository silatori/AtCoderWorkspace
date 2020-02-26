using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoderWorkspace.lib
{
    public struct ModInt
    {
        public const long Mod = (int)1e9 + 7;
        public long num;
        public ModInt(long n) { num = n; }
        public override string ToString() { return num.ToString(); }
        public static ModInt operator +(ModInt l, ModInt r) { l.num += r.num; if (l.num >= Mod) l.num -= Mod; return l; }
        public static ModInt operator -(ModInt l, ModInt r) { l.num -= r.num; if (l.num < 0) l.num += Mod; return l; }
        public static ModInt operator *(ModInt l, ModInt r) { return new ModInt(l.num * r.num % Mod); }
        public static ModInt operator /(ModInt l, ModInt r) { return l * Inverse(r); }
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
        private static List<ModInt> Facs = new List<ModInt> { 1 };
        public static ModInt Fac(int n)
        {
            for (int i = Facs.Count; i <= n; i++)
            {
                Facs.Add(i * Facs[i - 1]);
            }
            return Facs[n];
        }
        public static ModInt nCr(int n, int r)
        {
            if (n < r) return 0;
            if (n == r) return 1;
            return Fac(n) / (Fac(r) * Fac(n - r));
        }

        public static ModInt nCrON(int n, int r)
        {
            ModInt ans = 1;
            for (int i = 0; i < r; i++)
            {
                ans *= n - i;
            }
            for (int i = 1; i <= r; i++)
            {
                ans /= i;
            }
            return ans;
        }
    }
}
