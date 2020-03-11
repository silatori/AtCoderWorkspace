using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoderWorkspace.lib
{
    class BinarySearch<T>
    {
        T[] a;
        Func<T, bool> isOK;

        /// <param name="a">探索する配列</param>
        /// <param name="isOK">midの判定</param>
        public BinarySearch(T[] a, Func<T, bool> isOK)
        {
            this.a = a;
            this.isOK = isOK;
        }

        public int LSearch()
        {
            int ok = -1;
            int ng = a.Length;

            while (Math.Abs(ok - ng) > 1)
            {
                int mid = (ok + ng) / 2;

                if (isOK(a[mid])) ok = mid;
                else ng = mid;
            }
            return ok;
        }

        public int RSearch()
        {
            int ng = -1;
            int ok = a.Length;

            while (Math.Abs(ok - ng) > 1)
            {
                int mid = (ok + ng) / 2;

                if (isOK(a[mid])) ok = mid;
                else ng = mid;
            }
            return ok;
        }
    }

    class NBinarySearch
    {
        long ok;
        long ng;
        Func<long, bool> isOK;

        /// <param name="ok">必ず条件を満たすN</param>
        /// <param name="ng">必ず条件を満たさないN</param>
        /// <param name="isOK">midの判定</param>
        public NBinarySearch(long ok, long ng, Func<long, bool> isOK)
        {
            this.ok = ok;
            this.ng = ng;
            this.isOK = isOK;
        }

        public long OK()
        {
            long ok = this.ok;
            long ng = this.ng;

            while (Math.Abs(ok - ng) > 1)
            {
                long mid = (ok + ng) / 2;

                if (isOK(mid)) ok = mid;
                else ng = mid;
            }
            return ok;
        }

        public long NG()
        {
            long ok = this.ok;
            long ng = this.ng;

            while (Math.Abs(ok - ng) > 1)
            {
                long mid = (ok + ng) / 2;

                if (isOK(mid)) ok = mid;
                else ng = mid;
            }
            return ng;
        }
    }
}
