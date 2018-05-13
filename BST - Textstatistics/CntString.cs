using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextStatistics
{
    public class CntString
    {
        public string s;
        public int count;

        public CntString(string s, int count)
        {
            this.s = s;
            this.count = count;
        }
        public override string ToString()
        {
            return "(" + s + ", " + count + ") ";
        }


    }
}
