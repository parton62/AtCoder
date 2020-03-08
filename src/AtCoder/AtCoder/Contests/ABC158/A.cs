using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Contests.ABC158
{
    /// <summary>
    /// StationandBus
    /// </summary>
    static class A
    {
        public static void Main(string[] args)
        {
            var s = Console.ReadLine();

            if (s[0] == s[1] && s[0] == s[2]) Console.WriteLine("Yes");
            else Console.WriteLine("No");
        }
    }
}
