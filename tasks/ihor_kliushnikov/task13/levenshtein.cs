using System;

namespace ConsoleApplication1
{
    class Program
    {
        private static void Main()
        {
            const string s1 = "rick";
            const string s2 = "irkc";
            
            int n = s1.Length + 1;
            int m = s2.Length + 1;

            // Levenshtein
            var d = DefineArray(n, m);

            for (var i = 1; i < n; i++)
                for (var j = 1; j < m; j++)
                {
                    var i3 = d[i - 1][j - 1] + (s1[i-1] == s2[j-1] ? 0 : 1);
                    d[i][j] = GetMin(d[i-1][j] + 1, d[i][j-1] + 1, i3);
                }

            Console.WriteLine(d[n-1][m-1]); // returns 3

            // Damerau-Levenshtein
            var d2 = DefineArray(n, m);

            for (var i = 1; i < n; i++)
                for (var j = 1; j < m; j++)
                {
                    var i3 = d2[i - 1][j - 1] + (s1[i - 1] == s2[j - 1] ? 0 : 1);
                    d2[i][j] = GetMin(d2[i - 1][j] + 1, d2[i][j - 1] + 1, i3);

                    if (i > 1 && j > 1 && s1[i - 1] == s2[j - 2] && s1[i - 2] == s2[j - 1])
                        d2[i][j] = Math.Min(d2[i][j], d2[i - 2][j - 2] + 1);
                }

            Console.WriteLine(d2[n-1][m-1]); // returns 2
            Console.ReadKey();
        }

        private static int GetMin(int i1, int i2, int i3)
        {
            return Math.Min(Math.Min(i1, i2), i3);
        }

        private static int[][] DefineArray(int n, int m)
        {
            var array = new int[n][];
            
            for (var i = 0; i < n; i++)
            {
                array[i] = new int[m];
                array[i][0] = i;
            }
            
            for (var i = 0; i < m; i++)
                array[0][i] = i;

            return array;
        }
    }
}
