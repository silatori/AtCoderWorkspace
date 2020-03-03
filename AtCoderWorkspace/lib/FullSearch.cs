using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoderWorkspace.lib
{
    class FullSearch
    {
        public void BitSearch(int n)
        {
            for (int bit = 0; bit < (1 << n); bit++)
            {
                // 1count
                var count = 0;
                for (int i = 0; i < n; i++)
                    if ((bit & (1 << i)) > 0) count++;

                // check
                int ii = 0;
                if ((bit & (1 << ii)) > 0) { }
            }

            for (int bit = 0; bit < Math.Pow(2, n); bit++)
            {
                var bits = new bool[n];
                for (int i = 0; i < n; i++)
                {
                    var t = bit & (1 << i);
                    bits[n - i - 1] = t > 0; // 001 == bool{F,F,T}
                }
            }
        }
    }
}
