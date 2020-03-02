using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtCoderWorkspace.lib
{
    class UnionFind
    {
        public int Size { get; private set; }
        public int GroupCount { get; private set; }
        public IEnumerable<int> AllRepresents => Parent.Where((x, y) => x == y);
        int[] Sizes;
        int[] Parent;
        public UnionFind(int count)
        {
            Size = count;
            GroupCount = count;
            Parent = new int[count];
            Sizes = new int[count];
            for (int i = 0; i < count; i++) Sizes[Parent[i] = i] = 1;
        }
        public bool Unite(int x, int y)
        {
            int xp = Find(x);
            int yp = Find(y);
            if (yp == xp) return false;
            if (Sizes[xp] < Sizes[yp]) { var tmp = xp; xp = yp; yp = tmp; }
            GroupCount--;
            Parent[yp] = xp;
            Sizes[xp] += Sizes[yp];
            return true;
        }
        public int GetSize(int x) => Sizes[Find(x)];
        public int Find(int x)
        {
            while (x != Parent[x]) x = (Parent[x] = Parent[Parent[x]]);
            return x;
        }
        public bool Same(int x, int y) => Find(x) == Find(y);
    }
}
