using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> chrs = new List<char>();
            chrs.Add('в');
            chrs.Add('и');
            chrs.Add('к');
            chrs.Add('л');
            chrs.Add('м');
            chrs.Add('о');
            chrs.Add('о');
            chrs.Add('о');
            chrs.Add('т');
            Console.WriteLine(Other.StringListToString(Other.AnogramN(4, chrs)));
            Console.ReadKey();
        }
    }
}
