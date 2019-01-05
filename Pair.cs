using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{

    public class Pair<Type1, Type2>  
    {
        private Type1 first;
        private Type2 second;

        public Pair()
        {
            first = default(Type1);
            second = default(Type2);
        }

        public Pair(Type1 first, Type2 second)
        {
            this.first = first;
            this.second = second;
        }

        public Pair<Type1, Type2> GetPair { get { return this; } }

        public Type1 First { get { return this.first; } set { this.first = value; } }

        public Type2 Second { get { return this.second; } set { this.second = value; } }

        public override string ToString()
        {
            return "First: " + first.ToString() + "; Second: " + second.ToString();
        }
    }
}
