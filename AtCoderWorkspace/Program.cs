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
        public void Solve()
        {
            var cin = new Scanner();
            var h = cin.nextInt();
            var w = cin.nextInt();
            var f = new int[h, w];
            for (int i = 0; i < h; i++)
            {
                var row = cin.next().ToCharArray();
                for (int j = 0; j < w; j++)
                {
                    if (row[j] == '.')
                    {
                        f[i, j] = 0;
                    }
                    else
                    {
                        f[i, j] = 1;
                    }
                }
            }


            var ans = new List<int>();
            var xx = new int[] { 1, 0, -1, 0 }; // 右 下 左 上
            var yy = new int[] { 0, -1, 0, 1 };
            
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (f[i, j] == 1) continue; // スタートが壁は何もしない
                    for (int gi = 0; gi < h; gi++)
                    {
                        for (int gj = 0; gj < w; gj++)
                        {
                            if (f[gi, gj] == 1) continue; // ゴールが壁は何もしない
                            if (i == gi && j == gj) continue; // スタートゴールは違う場所
                            
                            var que = new Queue<point>();
                            que.Enqueue(new point() { x = j,y = i,cost = 0});
                            var visited = new int[h, w];
                            visited[i, j] = 1;
                            while (que.Count > 0)
                            {
                                var p = que.Dequeue();
                                // 右上左下チェック
                                for (int z = 0; z <= 3; z++)
                                {
                                    if(0 <= xx[z] + p.x && xx[z] + p.x < w && 0 <= yy[z] + p.y && yy[z] + p.y < h)
                                    {
                                        if (f[xx[z]+p.x, yy[z] + p.y] == 1) continue; // 壁は何もしない 
                                        if (visited[xx[z] + p.x, yy[z] + p.y] == 1) continue; // 訪問済みは何もしない

                                        // ゴール判定
                                        if(xx[z] + p.x　== gj && yy[z] + p.y == gi)
                                        {
                                            ans.Add(p.cost++);
                                            que.Clear();
                                            break;
                                        }
                                        else
                                        {
                                            visited[p.x,p.y] = 1;
                                            que.Enqueue(new point() { x = xx[z] + p.x, y = yy[z] + p.y, cost = p.cost++ });
                                        }                                        
                                    }                                    
                                }
                            }
                        }
                    }                   
                }
            }
            Console.WriteLine(ans.Max());
        }
    }

    class point
    {
        public int x { get; set; }
        public int y { get; set; }
        public int cost { get; set; }
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

        public int[] DPArrayInt(int N, int ex)
        {
            int[] Array = new int[N + ex];
            for (int i = 0; i < N; i++)
            {
                Array[i] = nextInt();
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

        public long[] DPArrayLong(int N, int ex)
        {
            long[] Array = new long[N + ex];
            for (int i = 0; i < N; i++)
            {
                Array[i] = nextLong();
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

    class Program
    {
        static void Main(string[] s) => new Solver().Solve();
    }
}
