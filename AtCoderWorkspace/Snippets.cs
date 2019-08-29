using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderWorkspace
{
    class Snippets
    {
        void LINQ()
        {
            //数値の各桁の和を計算
            var digitSum = 10
                .ToString()
                .ToCharArray()
                .Select(c => int.Parse(c.ToString()))
                .Sum();
        }
    }
}
