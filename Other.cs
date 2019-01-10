using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Other
    {
        static private List<string> _anogramm(List<char> chrs, int n)
        {
            List<string> ls = new List<string>();
            if (n == 0 || chrs.Count == 0) ls.Add("");
            else
            {
                int count = chrs.Count;
                for (int i = 0; i < count; i++)
                {
                    char chr = chrs[0];
                    chrs.RemoveAt(0);
                    List<string> temp = _anogramm(chrs, n - 1);
                    for (int k = 0; k < temp.Count; k++)
                        ls.Add(chr.ToString() + temp[k]);
                    chrs.Add(chr);
                }
            }
            return ls;
        }

        static public List<string> AnogramN(int n, List<char> chrs)
        {
            List<string> strs = new List<string>();
            strs = _anogramm(chrs, n);
            strs = strs.Distinct().ToList<string>();
            strs.Sort();
            return strs;
        }

        static public string StringListToString(List<string> strs)
        {
            string s = "";
            for (int i = 0; i < strs.Count; i++)
                s += strs[i] + "\n";
            return s;
        }
    }
}
