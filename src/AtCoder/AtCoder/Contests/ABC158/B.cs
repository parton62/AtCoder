using System;
using System.Collections.Generic;
using System.Text;

namespace AtCoder.Contests.ABC158
{
    /// <summary>
    /// 
    /// </summary>
    static class B
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            
            if (s[0] == s[1] && s[0] == s[2]) Console.WriteLine("No");
            else Console.WriteLine("Yes");
        }
    }
}
